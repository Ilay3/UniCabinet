﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Application.UseCases.DisciplineUseCase
{
    public class UpdateDisciplineUseCase
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IMapper _mapper;

        public UpdateDisciplineUseCase(IDisciplineRepository disciplineRepository, IMapper mapper)
        {
            _disciplineRepository = disciplineRepository;
            _mapper = mapper;
        }

        public bool Execute(DisciplineEditVM viewModel, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return false;
            }

            var disciplineDTO = _mapper.Map<DisciplineDTO>(viewModel);
            _disciplineRepository.UpdateDiscipline(disciplineDTO);
            return true;
        }
    }
}