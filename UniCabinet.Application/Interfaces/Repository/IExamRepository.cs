using UniCabinet.Core.DTOs.ExamManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IExamRepository
    {
        Task<ExamDTO> GetExamByIdAsync(int id);
        Task<List<ExamDTO>> GetExamListByDisciplineDetailIdAsync(int disciplineDetailId);
        Task AddExamAsync(ExamDTO examDTO);
        Task UpdateExamAsync(ExamDTO examDTO);
    }

}