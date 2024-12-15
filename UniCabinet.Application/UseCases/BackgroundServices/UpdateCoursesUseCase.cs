using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.BackgroundServices
{
    public class UpdateCoursesUseCase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<UpdateCoursesUseCase> _logger;


        public UpdateCoursesUseCase(IGroupRepository groupRepository, ILogger<UpdateCoursesUseCase> logger)
        {
            _groupRepository = groupRepository;
            _logger = logger;

        }
        public async Task Execute()
        {
            _logger.LogInformation("Начинается обновление курсов групп.");

            var groups = await _groupRepository.GetAllGroupsAsync();

            if (groups == null || !groups.Any())
            {
                _logger.LogInformation("Нет групп для обновления.");
                return;
            }

            var groupsToUpdate = new List<GroupDTO>();
            var usersToUpdate = new List<UserDTO>();

            foreach (var group in groups)
            {
                int maxCourse = group.TypeGroup == "Очно" ? 4 : 5;

                if (group.CourseId < maxCourse)
                {
                    // Увеличиваем курс на 1
                    group.CourseId += 1;
                    groupsToUpdate.Add(group);
                }
                else
                {
                    // Сбрасываем курс на 1
                    group.CourseId = 1;
                    groupsToUpdate.Add(group);

                    // Обнуляем GroupId у пользователей этой группы
                    var users = await _groupRepository.GetUsersByGroupIdAsync(group.Id);
                    if (users != null && users.Any())
                    {
                        foreach (var user in users)
                        {
                            user.GroupId = null;
                            usersToUpdate.Add(user);
                        }
                    }
                }
            }

            // Обновляем курсы групп
            await _groupRepository.UpdateGroupsCourseAsync(groupsToUpdate);
            _logger.LogInformation($"Обновлены курсы у {groupsToUpdate.Count} групп.");

            // Обновляем пользователей, обнуляя их GroupId
            if (usersToUpdate.Any())
            {
                await _groupRepository.UpdateUsersGroupAsync(usersToUpdate);
                _logger.LogInformation($"Обновлены связи группы у {usersToUpdate.Count} пользователей.");
            }

            _logger.LogInformation("Обновление курсов групп завершено.");
        }
    }
}
