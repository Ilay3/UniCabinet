using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.SpecializationManagement;

namespace UniCabinet.Application.UseCases.SpecializationUseCase
{
    public class GetSpecializationByIdUseCase
    {
        private readonly ISpecializationRepository _repository;

        public GetSpecializationByIdUseCase(ISpecializationRepository repository)
        {
            _repository = repository;
        }

        public async Task<SpecializationDTO> ExecuteAsync(int id)
        {
            return await _repository.GetSpecializationById(id);
        }
    }
}
