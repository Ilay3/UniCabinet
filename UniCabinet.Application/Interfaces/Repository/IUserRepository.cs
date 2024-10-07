﻿// UniCabinet.Application/Interfaces/IUserRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersWithRolesAsync();
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
        Task UpdateUserGroupAsync(string userId, int groupId);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<IEnumerable<User>> SearchUsersAsync(string query);
    }
}
