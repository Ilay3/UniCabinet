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

        public async Task<bool> ExecuteAsync(DisciplineDTO disciplineDTO, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return false;
            }

           await _disciplineRepository.AddDisciplineAsync(disciplineDTO);
            return true;
        }
    }
}
