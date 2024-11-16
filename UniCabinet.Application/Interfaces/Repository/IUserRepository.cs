// UniCabinet.Application/Interfaces/IUserRepository.cs
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAllUsersWithRolesAsync();
        Task<IEnumerable<UserEntity>> GetUsersByRoleAsync(string role);
        Task UpdateUserGroupAsync(string userId, int groupId);
        Task<IEnumerable<GroupEntity>> GetAllGroupsAsync();
        Task<IEnumerable<UserEntity>> SearchUsersAsync(string query);
        Task<UserEntity> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(UserEntity user);
        Task<UserDTO> GetUserById(string id);

        List<UserDTO> GetUsersByGroupId(int groupId);
        void UpdateUsersGroup(List<UserDTO> usersToUpdate);

        Task<List<UserDTO>> GetStudentsByGroupIdAsync(int groupId);

    }
}
