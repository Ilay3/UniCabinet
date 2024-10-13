// UniCabinet.Application/Interfaces/IUserService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task UpdateStudentGroupAsync(string userId, int groupId);
        Task UpdateUserRoleAsync(string userId, string role);
        Task<IEnumerable<User>> SearchUsersByNameOrEmailAndRoleAsync(string query, string role);
        Task<UserDetailDTO> GetUserDetailsAsync(string userId);
        Task UpdateUserDetailsAsync(UserDetailDTO model);
    }
}
