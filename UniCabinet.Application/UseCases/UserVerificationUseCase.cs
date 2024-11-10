using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.UseCases
{
    public class UserVerificationUseCase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserEntity> _logger;

        public UserVerificationUseCase(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, ILogger<UserEntity> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var isVerified = user.IsVerified();
                _logger.LogInformation($"Проверка пользователя {userId}: IsVerified = {isVerified}");

                if (isVerified)
                {
                    await AssignRoleAsync(userId, "Verified");
                    _logger.LogInformation($"Пользователю {userId} присвоена роль 'Verified'");

                    if (await _userManager.IsInRoleAsync(user, "Not Verified"))
                    {
                        await _userManager.RemoveFromRoleAsync(user, "Not Verified");
                        _logger.LogInformation($"У пользователя {userId} удалена роль 'Not Verified'");
                    }

                    return true;
                }
                else
                {
                    _logger.LogInformation($"Пользователь {userId} не прошел проверку IsVerified");
                }
            }
            else
            {
                _logger.LogError($"Пользователь с ID {userId} не найден");
            }
            return false;
        }
        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }

    }
}
