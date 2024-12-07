using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DisciplineDetailManagment;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.UseCases.DisciplineDetailUseCase
{
    public class AddDisciplineDetailUseCase
    {
        private readonly IDisciplineDetailRepository _repository;

        public AddDisciplineDetailUseCase(IDisciplineDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(DisciplineDetailDTO dto)
        {


            await _repository.AddAsync(dto);
        }
    }



}
