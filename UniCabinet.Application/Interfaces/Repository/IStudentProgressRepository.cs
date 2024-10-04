using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IStudentProgressRepository
    {
        Task AddStudentProgressAsync(StudentProgressDTO studentProgressDTO);
        Task DeleteStudentProgress(int id);
        List<StudentProgressDTO> GetAllStudentProgress();
        StudentProgressDTO GetStudentProfressById(int id);
        void UpdateStudentProgress(StudentProgressDTO studentProgressDTO);
    }
}