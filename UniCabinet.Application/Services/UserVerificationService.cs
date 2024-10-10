using System.Threading.Tasks;
using UniCabinet.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using UniCabinet.Application.Interfaces.Services;

namespace UniCabinet.Application.Services
{
    public class UserVerificationService : IUserVerificationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserVerificationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }

        public async Task<bool> VerifyUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.IsVerified())
            {
                await AssignRoleAsync(userId, "Verified");

                if (await _userManager.IsInRoleAsync(user, "Not Verified"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Not Verified");
                }

                return true;
            }
            return false;
        }
    }
}

