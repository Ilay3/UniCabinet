using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;

using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class UpdateUserDetailsUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UpdateUserDetailsUseCase> _logger;

        public UpdateUserDetailsUseCase(IUserService userService, ILogger<UpdateUserDetailsUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<bool> Execute(UserDetailDTO userDetailDTO)
        {
            try
            {
                _logger.LogInformation("Обновление деталей пользователя {UserId}.", userDetailDTO.Id);

                var userDTO = new UserDTO
                {
                    Id = userDetailDTO.Id,
                    Email = userDetailDTO.Email,
                    FirstName = userDetailDTO.FirstName,
                    LastName = userDetailDTO.LastName,
                    Patronymic = userDetailDTO.Patronymic,
                    DateBirthday = userDetailDTO.DateBirthday
                };

                await _userService.UpdateUserDetailsAsync(userDTO);

                _logger.LogInformation("Детали пользователя {UserId} успешно обновлены.", userDetailDTO.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении деталей пользователя {UserId}.", userDetailDTO.Id);
                return false;
            }
        }
    }
}