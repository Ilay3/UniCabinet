using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IExamRepository
    {
        Task AddExamAsync(ExamDTO examDTO);
        Task DeleteExam(int id);
        List<ExamDTO> GetAllExams();
        ExamDTO GetExamById(int id);
        void UpdateExam(ExamDTO examDTO);
    }
}