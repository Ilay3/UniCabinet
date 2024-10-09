using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Data;
using UniCabinet.Application.Interfaces;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
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
                    FullName = $"{user.FirstName} {user.LastName} {user.Patronymic}",
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

        public async Task<IEnumerable<GroupDTO>> GetAllGroupsAsync()
        {
            var groups = await _userRepository.GetAllGroupsAsync();

            return groups.Select(g => new GroupDTO
            {
                Id = g.Id,
                Name = g.Name,
                TypeGroup = g.TypeGroup,
                CourseId = g.CourseId,
                SemesterId = g.SemesterId
            }).ToList();
        }

        public async Task UpdateUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // Получаем все роли пользователя
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Удаляем текущие роли, кроме "Verified"
                var rolesToRemove = currentRoles.Where(r => r != "Verified").ToList();
                if (rolesToRemove.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                }

                // Добавляем новую роль, если она не "Verified"
                if (!string.IsNullOrEmpty(newRole) && newRole != "Verified")
                {
                    await _userManager.AddToRoleAsync(user, newRole);
                }
            }

        }

        public async Task<IEnumerable<User>> SearchUsersByNameOrEmailAndRoleAsync(string query, string role)
        {
            var users = await _userRepository.SearchUsersAsync(query);

            if (!string.IsNullOrEmpty(role))
            {
                users = users.Where(u => _userManager.IsInRoleAsync(u, role).Result).ToList(); // Фильтрация по роли
            }

            return users;
        }

        public async Task<UserDetailDTO> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userDetailDTO = new UserDetailDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                DateBirthday = user.DateBirthday,
                Roles = roles.ToList(),
                GroupName = user.Group != null ? user.Group.Name : "Без группы"
            };

            return userDetailDTO;
        }

        public async Task UpdateUserDetailsAsync(UserDetailDTO model)
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


    }
}
