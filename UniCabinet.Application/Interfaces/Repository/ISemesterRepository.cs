using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISemesterRepository
    {
        Task<List<SemesterDTO>> GetAllSemesters();
        Task<SemesterDTO> GetSemesterById(int id);
    }
}