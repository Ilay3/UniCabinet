using UniCabinet.Core.DTOs.ExamManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IExamResultRepository
    {
      
        Task AddOrUpdateExamResultAsync(ExamResultDTO examResultDTO);
        Task<List<ExamResultDTO>> GetExamResultsByExamIdAsync(int examId);
    }

}