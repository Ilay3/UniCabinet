// UniCabinet.Application/Interfaces/IUserService.cs
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task UpdateStudentGroupAsync(string userId, int groupId);
        Task UpdateUserRoleAsync(string userId, string role);
        Task<IEnumerable<UserEntity>> SearchUsersByNameOrEmailAndRoleAsync(string query, string role);
        Task UpdateUserDetailsAsync(UserDTO model);
        Task<UserDTO> GetUserByIdAsync(string userId);

    }
}
