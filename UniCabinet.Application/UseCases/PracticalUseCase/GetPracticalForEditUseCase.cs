using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Application.UseCases.PracticalUseCase
{
    public class GetPracticalForEditUseCase
    {
        private readonly IPracticalRepository _practicalRepository;
        private readonly IMapper _mapper;

        public GetPracticalForEditUseCase(
            IPracticalRepository practicalRepository,
            IMapper mapper)
        {
            _practicalRepository = practicalRepository;
            _mapper = mapper;
        }

        public async Task<PracticalDTO> ExecuteAsync(int id)
        {
            var practicalDTO = await _practicalRepository.GetPracticalByIdAsync(id);
            return practicalDTO;
        }
    }
}
