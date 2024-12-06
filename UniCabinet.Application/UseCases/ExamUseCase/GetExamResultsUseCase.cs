using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.ExamManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniCabinet.Application.UseCases.ExamUseCase
{
    public class GetExamResultsUseCase
    {
        private readonly IExamResultRepository _examResultRepository;

        public GetExamResultsUseCase(IExamResultRepository examResultRepository)
        {
            _examResultRepository = examResultRepository;
        }

        public async Task<List<ExamResultDTO>> ExecuteAsync(int examId)
        {
            return await _examResultRepository.GetExamResultsByExamIdAsync(examId);
        }
    }
}
