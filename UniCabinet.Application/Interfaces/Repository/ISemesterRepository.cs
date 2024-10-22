// Файл: UniCabinet.Application/Interfaces/Repository/ISemesterRepository.cs

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISemesterRepository
    {
        // Существующие синхронные методы
        List<SemesterDTO> GetAllSemesters();
        SemesterDTO GetSemesterById(int id);
        SemesterDTO GetCurrentSemester(DateTime currentDate);

        void Add(Semester semester);
        void Update(Semester semester);
        void Remove(Semester semester);

        // Добавленные асинхронные методы
        Task<List<SemesterDTO>> GetAllSemestersAsync();
        Task<SemesterDTO> GetSemesterByIdAsync(int id);
        Task<Semester> GetSemesterEntityByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
