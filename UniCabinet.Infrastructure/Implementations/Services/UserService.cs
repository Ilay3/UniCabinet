using Microsoft.AspNetCore.Identity;
using System.Data;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserEntity> _userManager;

        public UserService(IUserRepository userRepository, UserManager<UserEntity> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersWithRolesAsync();

            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    Roles = roles.ToList(),
                    GroupName = user.Group != null ? user.Group.Name : "Без группы"
                });
            }

            return userDTOs;
        }


        public async Task UpdateStudentGroupAsync(string userId, int groupId)
        {
            await _userRepository.UpdateUserGroupAsync(userId, groupId);
        }

        public async Task UpdateUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // Получаем все роли пользователя
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Удаляем текущие роли, кроме "Верефицирован"
                var rolesToRemove = currentRoles.Where(r => r != "Верефицирован").ToList();
                if (rolesToRemove.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                }

                // Добавляем новую роль, если она не "Верефицирован"
                if (!string.IsNullOrEmpty(newRole) && newRole != "Верефицирован")
                {
                    await _userManager.AddToRoleAsync(user, newRole);
                }
            }

        }

        public async Task<IEnumerable<UserEntity>> SearchUsersByNameOrEmailAndRoleAsync(string query, string role)
        {
            var users = await _userRepository.SearchUsersAsync(query);

            if (!string.IsNullOrEmpty(role))
            {
                users = users.Where(u => _userManager.IsInRoleAsync(u, role).Result).ToList(); // Фильтрация по роли
            }

            return users;
        }


        public async Task UpdateUserDetailsAsync(UserDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Patronymic = model.Patronymic;
                user.DateBirthday = model.DateBirthday;

                // Update in the database
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                DateBirthday = user.DateBirthday,
                Roles = roles.ToList(),
                GroupName = user.Group != null ? user.Group.Name : "Без группы",
                GroupId = user.GroupId,
                DepartmentId = user.DepartmentId
            };
        }
        public async Task UpdateUserSpecAndDepAsync(UserDTO userDto)
        {
            var userEntity = await _userRepository.GetUserByIdAsync(userDto.Id);
            if (userEntity == null)
            {
                throw new InvalidOperationException("Пользователь не найден.");
            }

            userEntity.DepartmentId = userDto.DepartmentId;
            userEntity.SpecialtyId = userDto.SpecializationId;

            await _userRepository.UpdateUserAsync(userEntity);
        }


    }
}
