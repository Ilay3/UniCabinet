using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISemesterRepository
    {
        // Существующие методы
        List<SemesterDTO> GetAllSemesters();
        SemesterDTO GetSemesterById(int id);
        SemesterDTO GetCurrentSemester(DateTime currentDate);
        SemesterEntity GetSemesterEntityById(int id);

        void Add(SemesterEntity semester);
        void Update(SemesterEntity semester);
        void Remove(SemesterEntity semester);

        void SaveChanges();
    }
}
