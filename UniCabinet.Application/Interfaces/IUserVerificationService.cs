using System.Threading.Tasks;

namespace UniCabinet.Application.Interfaces
{
    public interface IUserVerificationService
    {
        Task AssignRoleAsync(string userId, string roleName);
        Task<bool> VerifyUserAsync(string userId);
    }
}
