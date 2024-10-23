// Файл: UniCabinet.Application/Interfaces/Services/ISemesterService.cs

using System;
using System.Collections.Generic;
using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Services
{
    public interface ISemesterService
    {
        List<SemesterDTO> GetAllSemesters();
        SemesterDTO GetSemesterById(int id);
        void CreateSemester(SemesterDTO semesterDto);
        void UpdateSemester(SemesterDTO semesterDto);
        void DeleteSemester(int id);

        /// <summary>
        /// Обновление текущего семестра на основе текущей или тестовой даты
        /// </summary>
        /// <param name="testDate">Тестовая дата для обновления семестра (опционально)</param>
        Task UpdateCurrentSemesterAsync(DateTime? testDate = null);
    }
}
