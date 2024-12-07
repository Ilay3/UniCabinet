using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Application.UseCases.PracticalUseCase
{
    public class UpdatePracticalUseCase
    {
        private readonly IPracticalRepository _practicalRepository;

        public UpdatePracticalUseCase(IPracticalRepository practicalRepository)
        {
            _practicalRepository = practicalRepository;
        }

        public async Task ExecuteAsync(PracticalDTO practicalDTO)
        {
            await _practicalRepository.UpdatePracticalAsync(practicalDTO);
        }
    }
}
