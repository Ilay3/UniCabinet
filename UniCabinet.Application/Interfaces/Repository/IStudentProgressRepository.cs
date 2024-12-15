using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IStudentProgressRepository
    {
        void AddStudentProgress(StudentProgressDTO studentProgressDTO);
        void DeleteStudentProgress(int id);
        List<StudentProgressDTO> GetAllStudentProgress();
        StudentProgressDTO GetStudentProfressById(int id);
        void UpdateStudentProgress(StudentProgressDTO studentProgressDTO);
        Task<StudentProgressDTO> GetStudentProgressAsync(string studentId, int disciplineDetailId);
        Task AddStudentProgressAsync(StudentProgressDTO studentProgress);
        Task SaveChangesAsync();
    }
}