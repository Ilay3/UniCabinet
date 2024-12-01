using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.SpecializationManagement;

namespace UniCabinet.Application.UseCases.SpecializationUseCase
{
    public class UpdateSpecializationUseCase
    {
        private readonly ISpecializationRepository _repository;

        public UpdateSpecializationUseCase(ISpecializationRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(SpecializationEditDTO specializationDTO)
        {
            await _repository.UpdateSpecialization(specializationDTO);
        }
    }
}
