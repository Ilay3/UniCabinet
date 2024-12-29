using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISemesterRepository
    {
        Task<List<SemesterDTO>> GetAllSemesters();
        SemesterDTO GetSemesterById(int id);
        Task<SemesterDTO> GetCurrentSemesterAsync(DateTime currentDate);

    }
}
