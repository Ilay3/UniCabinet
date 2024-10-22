// Файл: UniCabinet.Application/Interfaces/Services/ISemesterService.cs

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Services
{
    public interface ISemesterService
    {
        /// <summary>
        /// Получить список всех семестров.
        /// </summary>
        /// <returns>Список DTO семестров.</returns>
        Task<List<SemesterDTO>> GetAllSemestersAsync();

        /// <summary>
        /// Получить семестр по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор семестра.</param>
        /// <returns>DTO семестра или null, если не найдено.</returns>
        Task<SemesterDTO> GetSemesterByIdAsync(int id);

        /// <summary>
        /// Создать новый семестр.
        /// </summary>
        /// <param name="semesterDto">DTO нового семестра.</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task CreateSemesterAsync(SemesterDTO semesterDto);

        /// <summary>
        /// Обновить существующий семестр.
        /// </summary>
        /// <param name="semesterDto">DTO семестра для обновления.</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task UpdateSemesterAsync(SemesterDTO semesterDto);

        /// <summary>
        /// Удалить семестр по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор семестра.</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task DeleteSemesterAsync(int id);

        /// <summary>
        /// Обновление текущего семестра на основе текущей или тестовой даты.
        /// </summary>
        /// <param name="testDate">Тестовая дата для обновления семестра (опционально).</param>
        /// <returns>Задача асинхронного выполнения.</returns>
        Task UpdateCurrentSemesterAsync(DateTime? testDate = null);
    }
}
