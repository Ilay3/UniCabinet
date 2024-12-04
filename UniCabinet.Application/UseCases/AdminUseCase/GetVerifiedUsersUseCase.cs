using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Common;
using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Core.Models;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class GetVerifiedUsersUseCase
    {
        private readonly IUserService _userService;
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<GetVerifiedUsersUseCase> _logger;

        public GetVerifiedUsersUseCase(IUserService userService, IGroupRepository groupRepository, ILogger<GetVerifiedUsersUseCase> logger)
        {
            _userService = userService;
            _groupRepository = groupRepository;
            _logger = logger;
        }

        public async Task<StudentGroupDTO> Execute(string role, string query, int pageNumber, int pageSize)
        {
            _logger.LogInformation("Получение проверенных пользователей.");

            if (string.IsNullOrEmpty(role))
            {
                role = "Студент"; // По умолчанию выбираем роль Student
            }

            // Получаем всех пользователей (DTO)
            var userDTOs = await _userService.GetAllUsersAsync();

            // Фильтрация по роли
            if (role == "Верефицирован")
            {
                userDTOs = userDTOs.Where(user => user.Roles.Count == 1 && user.Roles.Contains("Верефицирован")).ToList();
            }
            else
            {
                userDTOs = userDTOs.Where(user => user.Roles.Contains(role)).ToList();
            }

            // Фильтрация по запросу
            if (!string.IsNullOrEmpty(query))
            {
                userDTOs = userDTOs
                    .Where(user =>
                        (user.FirstName != null && user.FirstName.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                        (user.LastName != null && user.LastName.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                        (user.Patronymic != null && user.Patronymic.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                        user.Email.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            // Пагинация
            var totalUsers = userDTOs.Count();
            var paginatedUsers = userDTOs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Получение всех групп
            var groups = await _groupRepository.GetAllGroupsAsync();

            // Подготовка модели пагинации
            var paginationModel = new PaginationModel
            {
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize),
                Action = "VerifiedUsers",
                Controller = "Admin",
                RouteValues = new PaginationRouteValues
                {
                    Role = role,
                    PageSize = pageSize,
                    Query = query
                }
            };

            // Создание DTO модели
            var model = new StudentGroupDTO
            {
                Users = paginatedUsers,
                Groups = groups.Select(g => new SelectListItemDTO
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = false
                }).ToList(),
                Pagination = paginationModel
            };

            _logger.LogInformation("Получение проверенных пользователей завершено.");

            return model;
        }
    }
}
