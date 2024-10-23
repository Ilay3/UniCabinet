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

        public List<SemesterDTO> GetAllSemesters()
        {
            return _semesterRepository.GetAllSemesters();
        }

        public SemesterDTO GetSemesterById(int id)
        {
            return _semesterRepository.GetSemesterById(id);
        }

        public void CreateSemester(SemesterDTO semesterDto)
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
            _semesterRepository.SaveChanges();
        }

        public void UpdateSemester(SemesterDTO semesterDto)
        {
            var semesterEntity = _semesterRepository.GetSemesterEntityById(semesterDto.Id);
            if (semesterEntity != null)
            {
                semesterEntity.Number = semesterDto.Number;
                semesterEntity.DayStart = semesterDto.DayStart;
                semesterEntity.MounthStart = semesterDto.MounthStart;
                semesterEntity.DayEnd = semesterDto.DayEnd;
                semesterEntity.MounthEnd = semesterDto.MounthEnd;

                _semesterRepository.Update(semesterEntity);
                _semesterRepository.SaveChanges();
            }
        }

        public void DeleteSemester(int id)
        {
            var semester = _semesterRepository.GetSemesterById(id);
            if (semester != null)
            {
                _semesterRepository.Remove(new Semester { Id = id });
                _semesterRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Обновление текущего семестра на основе текущей или тестовой даты
        /// </summary>
        /// <param name="testDate">Тестовая дата для обновления семестра (опционально)</param>
        public async Task UpdateCurrentSemesterAsync(DateTime? testDate = null)
        {
            var currentDate = testDate ?? DateTime.Now;

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
