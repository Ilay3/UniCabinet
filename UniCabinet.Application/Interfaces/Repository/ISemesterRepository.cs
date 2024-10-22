using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISemesterRepository
    {
        Task<List<SemesterDTO>> GetAllSemestersAsync();
        Task<SemesterDTO> GetSemesterByIdAsync(int id);

        Task SaveChangesAsync();
        void Remove(Semester semester);
        void Update(Semester semester);
        void Add(Semester semester);
        Task<SemesterDTO> GetCurrentSemesterAsync(DateTime currentDate);
        Task<Semester> GetSemesterEntityByIdAsync(int id);
    }
}