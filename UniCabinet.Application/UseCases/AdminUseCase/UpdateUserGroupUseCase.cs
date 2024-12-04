using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class UpdateUserGroupUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UpdateUserGroupUseCase> _logger;

        public UpdateUserGroupUseCase(IUserService userService, ILogger<UpdateUserGroupUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<(bool Success, string ErrorMessage)> Execute(string userId, int? groupId, IList<string> userRoles)
        {
            if (string.IsNullOrEmpty(userId) || groupId == null)
            {
                _logger.LogWarning("Некорректные данные для обновления группы пользователя {UserId}.", userId);
                return (false, "Некорректные данные.");
            }

            // Проверяем, что пользователь имеет роли "Студент" и "Verified"
            if (!userRoles.Contains("Студент") || !userRoles.Contains("Верефицирован"))
            {
                _logger.LogWarning("Пользователь {UserId} не имеет необходимых ролей для изменения группы.", userId);
                return (false, "Изменение группы доступно только для пользователей с ролями Студент и Верефицирован.");
            }

            await _userService.UpdateStudentGroupAsync(userId, groupId.Value);

            _logger.LogInformation("Группа пользователя {UserId} успешно обновлена.", userId);
            return (true, null);
        }
    }
}
