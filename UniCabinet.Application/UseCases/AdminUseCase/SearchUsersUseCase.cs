using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.UserManagement;
namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class SearchUsersUseCase
    {
        private readonly IUserService _userService;
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<SearchUsersUseCase> _logger;

        public SearchUsersUseCase(IUserService userService, IGroupRepository groupRepository, ILogger<SearchUsersUseCase> logger)
        {
            _userService = userService;
            _groupRepository = groupRepository;
            _logger = logger;
        }

        public async Task<List<UserDTO>> Execute(string query, string role)
        {
            _logger.LogInformation("Поиск пользователей.");

            if (string.IsNullOrEmpty(query))
            {
                _logger.LogInformation("Пустой поисковый запрос.");
                return new List<UserDTO>();
            }

            var userDTOs = await _userService.GetAllUsersAsync();

            // Фильтрация по роли, если необходимо
            if (!string.IsNullOrEmpty(role))
            {
                if (role == "Верефицирован")
                {
                    userDTOs = userDTOs.Where(user => user.Roles.Count == 1 && user.Roles.Contains("Верефицирован")).ToList();
                }
                else
                {
                    userDTOs = userDTOs.Where(user => user.Roles.Contains(role)).ToList();
                }
            }

            // Фильтрация по запросу
            var filteredUsers = userDTOs
                .Where(user =>
                    (user.FirstName != null && user.FirstName.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                    (user.LastName != null && user.LastName.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                    (user.Patronymic != null && user.Patronymic.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                    user.Email.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    DateBirthday = user.DateBirthday,
                    Roles = user.Roles,
                    GroupName = user.GroupName,
                    GroupId = user.GroupId,
                })
                .ToList();

            _logger.LogInformation($"Найдено {filteredUsers.Count} пользователей по запросу '{query}'.");

            return filteredUsers;
        }
    }
}
