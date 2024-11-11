using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Application.UseCases.DisciplineUseCase
{
    public class GetDisciplineForEditUseCase
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;

        public GetDisciplineForEditUseCase(IDisciplineRepository disciplineRepository, IMapper mapper)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
        }

        public DisciplineEditVM Execute(int id)
        {
            var disciplineDTO = _disciplineRepository.GetDisciplineByIdAsync(id);
            if (disciplineDTO == null)
            {
                return null;
            }

            var disciplineVM = _mapper.Map<DisciplineEditVM>(disciplineDTO);
            return disciplineVM;
        }
    }
}
