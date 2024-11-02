// UniCabinet.Application/Interfaces/IUserRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.DTO;
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
        Task<User> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
        Task<UserDTO> GetUserById(string id);

        List<UserDTO> GetUsersByGroupId(int groupId);
        void UpdateUsersGroup(List<UserDTO> usersToUpdate);

        List<UserDTO> GetStudentsByGroupId(int groupId);

    }
}
