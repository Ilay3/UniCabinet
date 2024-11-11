using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Application.UseCases.DisciplineUseCase
{
    public class GetDisciplinesListUseCase
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;

        public GetDisciplinesListUseCase(IDisciplineRepository disciplineRepository, IMapper mapper)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
        }

        public async Task<List<DisciplineDTO>> ExecuteAsync()
        {
            var disciplineDTOs = await _disciplineRepository.GetAllDisciplinesAsync();
            return disciplineDTOs;
        }
    }
}
