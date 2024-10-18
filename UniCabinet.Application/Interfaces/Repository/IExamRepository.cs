using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IExamRepository
    {
        void AddExam(ExamDTO examDTO);
        void DeleteExam(int id);
        List<ExamDTO> GetAllExams();
        ExamDTO GetExamById(int id);
        void UpdateExam(ExamDTO examDTO);
    }
}