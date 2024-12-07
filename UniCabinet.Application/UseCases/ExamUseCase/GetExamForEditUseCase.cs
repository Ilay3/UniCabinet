using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.ExamManagement;
using System.Threading.Tasks;

namespace UniCabinet.Application.UseCases.ExamUseCase
{
    public class GetExamForEditUseCase
    {
        private readonly IExamRepository _examRepository;

        public GetExamForEditUseCase(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<ExamDTO> ExecuteAsync(int id)
        {
            return await _examRepository.GetExamByIdAsync(id);
        }
    }
}
