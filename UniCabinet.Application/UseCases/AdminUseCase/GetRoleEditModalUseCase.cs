using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.Common;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class GetRoleEditModalUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetRoleEditModalUseCase> _logger;

        public GetRoleEditModalUseCase(IUserService userService, ILogger<GetRoleEditModalUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<UserRolesDTO> Execute(string userId)
        {
            _logger.LogInformation("Получение данных для редактирования ролей пользователя {UserId}.", userId);

            var userDTO = await _userService.GetUserByIdAsync(userId);
            if (userDTO == null)
            {
                _logger.LogWarning("Пользователь с ID {UserId} не найден.", userId);
                return null;
            }

            var roles = new List<string> { "Студент", "Преподаватель", "Администратор", "Верефицирован" };

            var dto = new UserRolesDTO
            {
                UserId = userDTO.Id,
                FullName = $"{userDTO.FirstName} {userDTO.LastName} {userDTO.Patronymic}".Trim(),
                SelectedRoles = userDTO.Roles,
                AvailableRoles = roles.Select(r => new SelectListItemDTO
                {
                    Value = r,
                    Text = r,
                    Selected = userDTO.Roles.Contains(r)
                }).ToList()
            };

            _logger.LogInformation("Данные для редактирования ролей пользователя {UserId} получены.", userId);

            return dto;
        }
    }
}
