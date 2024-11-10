using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces.Repository;

namespace UniCabinet.Application.UseCases
{
    public class UpdateCurrentSemesterAsyncUseCase
    {
        private readonly ILogger<UpdateCurrentSemesterAsyncUseCase> _logger;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IGroupRepository _groupRepository;
        public UpdateCurrentSemesterAsyncUseCase(ISemesterRepository semesterRepository, IGroupRepository groupRepository, ILogger<UpdateCurrentSemesterAsyncUseCase> logger)
        {
            _semesterRepository = semesterRepository;
            _groupRepository = groupRepository;
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            var currentDate = DateTime.Now;

            // Получаем текущий семестр
            var currentSemester = _semesterRepository.GetCurrentSemester(currentDate);
            _logger.LogInformation($"Текущий семестр: №{currentSemester.Number}, период: {currentSemester.DayStart}.{currentSemester.MounthStart} - {currentSemester.DayEnd}.{currentSemester.MounthEnd}");

            // Получаем все группы
            var groups = _groupRepository.GetAllGroups();
            var groupsToUpdate = groups.Where(g => g.SemesterId != currentSemester.Id).ToList();

            if (groupsToUpdate.Any())
            {
                _logger.LogInformation($"Начинаем обновление {groupsToUpdate.Count} групп до семестра №{currentSemester.Number}");

                // Обновляем SemesterId у групп
                foreach (var group in groupsToUpdate)
                {
                    group.SemesterId = currentSemester.Id;
                }

                await _groupRepository.UpdateGroupsSemesterAsync(groupsToUpdate);
                await _groupRepository.SaveChangesAsync();

                _logger.LogInformation($"Обновление групп завершено: {groupsToUpdate.Count} групп обновлено до семестра №{currentSemester.Number}");
            }
            else
            {
                _logger.LogInformation("Нет групп для обновления.");
            }
        }
    }
}
