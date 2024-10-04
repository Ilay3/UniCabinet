using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IExamResultRepository
    {
        Task AddExamResultAsync(ExamResultDTO examResultDTO);
        Task DeleteExamResult(int id);
        List<ExamResultDTO> GetAllExamResults();
        ExamResultDTO GetExamResultById(int id);
        void UpdateExamResult(ExamResultDTO examResultDTO);
    }
}