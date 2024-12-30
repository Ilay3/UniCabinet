// UniCabinet.Application/Interfaces/IUserRepository.cs
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAllUsersWithRolesAsync();
        Task UpdateUserGroupAsync(string userId, int groupId);
        Task<IEnumerable<UserEntity>> SearchUsersAsync(string query);
        Task<UserEntity> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(UserEntity user);

        Task<List<UserDTO>> GetStudentsByGroupIdAsync(int groupId);

    }
}
