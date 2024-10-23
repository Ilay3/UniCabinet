using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISemesterRepository
    {
        // Существующие методы
        List<SemesterDTO> GetAllSemesters();
        SemesterDTO GetSemesterById(int id);
        SemesterDTO GetCurrentSemester(DateTime currentDate);
        Semester GetSemesterEntityById(int id);

        void Add(Semester semester);
        void Update(Semester semester);
        void Remove(Semester semester);

        void SaveChanges();
    }
}
