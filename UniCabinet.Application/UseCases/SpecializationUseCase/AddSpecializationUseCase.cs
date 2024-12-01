using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.SpecializationManagement;

namespace UniCabinet.Application.UseCases.SpecializationUseCase
{
    public class AddSpecializationUseCase
    {
        private readonly ISpecializationRepository _repository;

        public AddSpecializationUseCase(ISpecializationRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(SpecializationAddDTO specializationDTO)
        {
            await _repository.AddSpecialization(specializationDTO);
        }
    }
}
