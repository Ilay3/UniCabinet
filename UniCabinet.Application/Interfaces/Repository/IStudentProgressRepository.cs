using UniCabinet.Core.DTOs.StudentManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IStudentProgressRepository
    {
        void AddStudentProgress(StudentProgressDTO studentProgressDTO);
        void DeleteStudentProgress(int id);
        List<StudentProgressDTO> GetAllStudentProgress();
        StudentProgressDTO GetStudentProfressById(int id);
        void UpdateStudentProgress(StudentProgressDTO studentProgressDTO);
    }
}