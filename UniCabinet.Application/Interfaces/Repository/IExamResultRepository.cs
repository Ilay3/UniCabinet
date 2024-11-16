using UniCabinet.Core.DTOs.ExamManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IExamResultRepository
    {
        void AddExamResult(ExamResultDTO examResultDTO);
        void DeleteExamResult(int id);
        List<ExamResultDTO> GetAllExamResults();
        ExamResultDTO GetExamResultById(int id);
        void UpdateExamResult(ExamResultDTO examResultDTO);
    }
}