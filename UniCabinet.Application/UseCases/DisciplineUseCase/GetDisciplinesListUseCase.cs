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

        public List<DisciplineListVM> Execute()
        {
            var disciplineDTOs = _disciplineRepository.GetAllDisciplines();
            var disciplineVMs = _mapper.Map<List<DisciplineListVM>>(disciplineDTOs);
            return disciplineVMs;
        }
    }
}
