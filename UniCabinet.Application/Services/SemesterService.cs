// Файл: UniCabinet.Application/Services/SemesterService.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace UniCabinet.Application.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<SemesterService> _logger;

        public SemesterService(ISemesterRepository semesterRepository, IGroupRepository groupRepository, ILogger<SemesterService> logger)
        {
            _semesterRepository = semesterRepository;
            _groupRepository = groupRepository;
            _logger = logger;
        }

        public async Task<List<SemesterDTO>> GetAllSemestersAsync()
        {
            return await _semesterRepository.GetAllSemestersAsync();
        }

        public async Task<SemesterDTO> GetSemesterByIdAsync(int id)
        {
            return await _semesterRepository.GetSemesterByIdAsync(id);
        }

        public async Task CreateSemesterAsync(SemesterDTO semesterDto)
        {
            var semester = new Semester
            {
                Number = semesterDto.Number,
                DayStart = semesterDto.DayStart,
                MounthStart = semesterDto.MounthStart,
                DayEnd = semesterDto.DayEnd,
                MounthEnd = semesterDto.MounthEnd
            };

            _semesterRepository.Add(semester);
            await _semesterRepository.SaveChangesAsync();
        }

        public async Task UpdateSemesterAsync(SemesterDTO semesterDto)
        {
            var semesterEntity = await _semesterRepository.GetSemesterEntityByIdAsync(semesterDto.Id);
            if (semesterEntity != null)
            {
                semesterEntity.Number = semesterDto.Number;
                semesterEntity.DayStart = semesterDto.DayStart;
                semesterEntity.MounthStart = semesterDto.MounthStart;
                semesterEntity.DayEnd = semesterDto.DayEnd;
                semesterEntity.MounthEnd = semesterDto.MounthEnd;

                _semesterRepository.Update(semesterEntity);
                await _semesterRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteSemesterAsync(int id)
        {
            var semester = await _semesterRepository.GetSemesterByIdAsync(id);
            if (semester != null)
            {
                _semesterRepository.Remove(new Semester { Id = id });
                await _semesterRepository.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Обновление текущего семестра на основе текущей или тестовой даты
        /// </summary>
        /// <param name="testDate">Тестовая дата для обновления семестра (опционально)</param>
        /// <returns></returns>
        public async Task UpdateCurrentSemesterAsync(DateTime? testDate = null)
        {
            var currentDate = testDate ?? DateTime.Now;
            SemesterDTO currentSemester;

            try
            {
                currentSemester = _semesterRepository.GetCurrentSemester(currentDate);
                _logger.LogInformation($"Текущий семестр: №{currentSemester.Number}, период: {currentSemester.DayStart}.{currentSemester.MounthStart} - {currentSemester.DayEnd}.{currentSemester.MounthEnd}");
            }
            catch (InvalidOperationException ex)
            {
                // Логирование ошибки
                _logger.LogError(ex, "Текущий семестр не найден.");
                return;
            }

            // Получаем все семестры
            var semesters = await _semesterRepository.GetAllSemestersAsync();
            var previousSemester = semesters.OrderByDescending(s => s.Number).FirstOrDefault();

            if (previousSemester == null || previousSemester.Number != currentSemester.Number)
            {
                // Семестр сменился, обновляем связанные группы
                _logger.LogInformation($"Смена семестра: предыдущий семестр №{previousSemester?.Number}, новый семестр №{currentSemester.Number}");

                var groups = await _groupRepository.GetAllGroupsAsync();
                var groupsToUpdate = groups.Where(g => g.SemesterId != currentSemester.Id).ToList();

                if (groupsToUpdate.Any())
                {
                    _logger.LogInformation($"Начинаем обновление {groupsToUpdate.Count} групп до семестра №{currentSemester.Number}");

                    await _groupRepository.UpdateGroupsSemesterAsync(groupsToUpdate);
                    await _groupRepository.SaveChangesAsync();

                    _logger.LogInformation($"Обновление групп завершено: {groupsToUpdate.Count} групп обновлено до семестра №{currentSemester.Number}");
                }
                else
                {
                    _logger.LogInformation("Нет групп для обновления.");
                }
            }
            else
            {
                _logger.LogInformation("Смена семестра не требуется.");
            }
        }
    }
}
