using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.ExamManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniCabinet.Application.UseCases.ExamUseCase
{
    public class GetExamListByDisciplineUseCase
    {
        private readonly IExamRepository _examRepository;

        public GetExamListByDisciplineUseCase(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<List<ExamDTO>> ExecuteAsync(int disciplineDetailId)
        {
            return await _examRepository.GetExamListByDisciplineDetailIdAsync(disciplineDetailId);
        }
    }
}
