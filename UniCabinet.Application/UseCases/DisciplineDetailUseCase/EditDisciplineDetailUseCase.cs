using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DisciplineDetailManagment;

namespace UniCabinet.Application.UseCases.DisciplineDetailUseCase
{

    public class EditDisciplineDetailUseCase
    {
        private readonly IDisciplineDetailRepository _repository;

        public EditDisciplineDetailUseCase(IDisciplineDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(DisciplineDetailDTO dto)
        {
            await _repository.UpdateAsync(dto);   
        }
    }
}
