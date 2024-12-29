using UniCabinet.Core.DTOs.StudentProgressManagment;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IStudentProgressRepository
    {
        Task<List<StudentProgressDTO>> GetAllStudentProgressById(string studentId);
        Task<StudentProgressDTO> GetStudentProgressAsync(string studentId, int disciplineDetailId);
        Task AddStudentProgressAsync(StudentProgressDTO studentProgress);
        Task UpdateFinalGradeAsync(string studentId, decimal finalGrade);
        Task SaveChangesAsync();
    }
}