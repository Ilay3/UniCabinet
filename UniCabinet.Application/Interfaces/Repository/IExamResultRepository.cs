using UniCabinet.Core.DTOs.ExamManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IExamResultRepository
    {
        Task<ExamResultDTO> GetExamResultByIdAsync(int id);
        Task<List<ExamResultDTO>> GetAllExamResultsAsync();
        Task AddExamResultAsync(ExamResultDTO examResultDTO);
        Task DeleteExamResultAsync(int id);
        Task UpdateExamResultAsync(ExamResultDTO examResultDTO);
        Task AddOrUpdateExamResultAsync(ExamResultDTO examResultDTO);
        Task<List<ExamResultDTO>> GetExamResultsByExamIdAsync(int examId);
    }

}