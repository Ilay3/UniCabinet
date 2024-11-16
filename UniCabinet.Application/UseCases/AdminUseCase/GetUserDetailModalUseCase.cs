using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class GetUserDetailModalUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUserDetailModalUseCase> _logger;

        public GetUserDetailModalUseCase(IUserService userService, ILogger<GetUserDetailModalUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<UserDetailDTO> Execute(string userId)
        {
            _logger.LogInformation("Получение деталей пользователя {UserId}.", userId);

            var userDTO = await _userService.GetUserByIdAsync(userId);
            if (userDTO == null)
            {
                _logger.LogWarning("Пользователь с ID {UserId} не найден.", userId);
                return null;
            }

            var dto = new UserDetailDTO
            {
                Id = userDTO.Id,
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Patronymic = userDTO.Patronymic,
                DateBirthday = userDTO.DateBirthday
            };

            _logger.LogInformation("Детали пользователя {UserId} получены.", userId);

            return dto;
        }
    }
}
