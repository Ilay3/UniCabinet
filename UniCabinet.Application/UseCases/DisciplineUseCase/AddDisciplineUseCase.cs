using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Application.UseCases.DisciplineUseCase
{
    public class AddDisciplineUseCase
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;

        public AddDisciplineUseCase(IDisciplineRepository disciplineRepository, IMapper mapper)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
        }

        public bool Execute(DisciplineAddVM viewModel, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return false;
            }

            var disciplineDTO = _mapper.Map<DisciplineDTO>(viewModel);
            _disciplineRepository.AddDiscipline(disciplineDTO);
            return true;
        }
    }
}
