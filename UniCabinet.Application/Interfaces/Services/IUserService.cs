// UniCabinet.Application/Interfaces/IUserService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}
