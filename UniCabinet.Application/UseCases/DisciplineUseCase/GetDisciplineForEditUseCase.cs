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

        public async Task<DisciplineDTO> ExecuteAsync(int id)
        {
            var disciplineDTO = await _disciplineRepository.GetDisciplineByIdAsync(id);
            if (disciplineDTO == null)
            {
                return null;
            }

            return disciplineDTO;
        }
    }
}
