// UniCabinet.Application/Interfaces/IUserRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetVerifiedUsersAsync();
    }
}
