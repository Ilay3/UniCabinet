// UniCabinet.Application/Services/UserService.cs
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


    }
}
