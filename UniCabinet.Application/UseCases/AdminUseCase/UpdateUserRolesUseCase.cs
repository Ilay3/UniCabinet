using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class UpdateUserRolesUseCase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<UpdateUserRolesUseCase> _logger;

        public UpdateUserRolesUseCase(UserManager<UserEntity> userManager, ILogger<UpdateUserRolesUseCase> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<(bool Success, string ErrorMessage)> Execute(UserRolesDTO userRolesDTO)
        {
            var user = await _userManager.FindByIdAsync(userRolesDTO.UserId);
            if (user == null)
            {
                _logger.LogWarning("Пользователь с ID {UserId} не найден при обновлении ролей.", userRolesDTO.UserId);
                return (false, "Пользователь не найден.");
            }

            // Получаем текущие роли пользователя
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Роли, которые нужно добавить
            var rolesToAdd = userRolesDTO.SelectedRoles.Except(currentRoles).ToList();

            // Роли, которые нужно удалить
            var rolesToRemove = currentRoles.Except(userRolesDTO.SelectedRoles).ToList();

            // Удаление ролей
            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    _logger.LogError("Ошибка при удалении ролей для пользователя {UserId}.", userRolesDTO.UserId);
                    return (false, "Ошибка при удалении ролей.");
                }
            }

            // Добавление новых ролей
            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    _logger.LogError("Ошибка при добавлении ролей для пользователя {UserId}.", userRolesDTO.UserId);
                    return (false, "Ошибка при добавлении ролей.");
                }
            }

            _logger.LogInformation("Роли пользователя {UserId} успешно обновлены.", userRolesDTO.UserId);
            return (true, null);
        }
    }
}
