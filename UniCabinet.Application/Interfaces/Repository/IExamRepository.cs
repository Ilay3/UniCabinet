using UniCabinet.Core.DTOs.ExamManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IExamRepository
    {
        Task<ExamDTO> GetExamByIdAsync(int id);
        Task<List<ExamDTO>> GetExamListByDisciplineDetailIdAsync(int disciplineDetailId);
        Task<List<ExamDTO>> GetAllExamsAsync();
        Task AddExamAsync(ExamDTO examDTO);
        Task DeleteExamAsync(int id);
        Task UpdateExamAsync(ExamDTO examDTO);
    }

}