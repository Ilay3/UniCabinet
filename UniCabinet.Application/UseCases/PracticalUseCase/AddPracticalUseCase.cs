using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Application.UseCases.PracticalUseCase
{
    public class AddPracticalUseCase
    {
        private readonly IPracticalRepository _practicalRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IMapper _mapper;

        public AddPracticalUseCase(
            IPracticalRepository practicalRepository,
            IDisciplineDetailRepository disciplineDetailRepository,
            IMapper mapper)
        {
            _practicalRepository = practicalRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(PracticalDTO practicalDTO, ModelStateDictionary modelState)
        {
            var existingPracticalsCount = await _practicalRepository.GetPracticalCountByDisciplineDetailIdAsync(practicalDTO.DisciplineDetailId);
            var disciplineDetail = await _disciplineDetailRepository.GetDisciplineDetailByIdAsync(practicalDTO.DisciplineDetailId);
            int maxPracticals = disciplineDetail.PracticalCount;

            if (existingPracticalsCount >= maxPracticals)
            {
                modelState.AddModelError("", "Достигнуто максимальное количество практических работ.");
                return false;
            }

            await _practicalRepository.AddPracticalAsync(practicalDTO);

            return true;
        }
    }
}
