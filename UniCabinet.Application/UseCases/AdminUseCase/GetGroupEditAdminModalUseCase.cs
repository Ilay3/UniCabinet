using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Common;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class GetGroupEditAdminModalUseCase
    {
        private readonly IUserService _userService;
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<GetGroupEditAdminModalUseCase> _logger;

        public GetGroupEditAdminModalUseCase(IUserService userService, IGroupRepository groupRepository, ILogger<GetGroupEditAdminModalUseCase> logger)
        {
            _userService = userService;
            _groupRepository = groupRepository;
            _logger = logger;
        }

        public async Task<UserGroupDTO> Execute(string userId)
        {
            _logger.LogInformation("Получение данных для редактирования группы пользователя {UserId}.", userId);

            var userDTO = await _userService.GetUserByIdAsync(userId);
            if (userDTO == null)
            {
                _logger.LogWarning("Пользователь с ID {UserId} не найден.", userId);
                return null;
            }

            var groups = await _groupRepository.GetAllGroupsAsync();

            var dto = new UserGroupDTO
            {
                UserId = userDTO.Id,
                FullName = $"{userDTO.FirstName} {userDTO.LastName} {userDTO.Patronymic}".Trim(),
                GroupId = userDTO.GroupId,
                AvailableGroups = groups.Select(g => new SelectListItemDTO
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = g.Id == userDTO.GroupId
                }).ToList()
            };

            _logger.LogInformation("Данные для редактирования группы пользователя {UserId} получены.", userId);

            return dto;
        }
    }
}
