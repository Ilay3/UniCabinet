using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISemesterRepository
    {
        List<SemesterDTO> GetAllSemesters();
        SemesterDTO GetSemesterById(int id);
    }
}