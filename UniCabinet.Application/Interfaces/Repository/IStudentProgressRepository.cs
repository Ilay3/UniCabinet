using UniCabinet.Core.DTOs.StudentProgressManagment;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IStudentProgressRepository
    {
        void AddStudentProgress(StudentProgressDTO studentProgressDTO);
        void DeleteStudentProgress(int id);
        List<StudentProgressDTO> GetAllStudentProgress();
         Task<List<StudentProgressDTO>> GetAllStudentProgressById(string studentId);
        void UpdateStudentProgress(StudentProgressDTO studentProgressDTO);
        Task<StudentProgressDTO> GetStudentProgressAsync(string studentId, int disciplineDetailId);
        Task AddStudentProgressAsync(StudentProgressDTO studentProgress);
        Task UpdateFinalGradeAsync(string studentId, decimal finalGrade);
        Task SaveChangesAsync();
    }
}