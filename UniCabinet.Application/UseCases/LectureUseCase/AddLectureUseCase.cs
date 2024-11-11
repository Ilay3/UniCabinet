using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Lecture;

namespace UniCabinet.Application.UseCases.LectureUseCase
{
    public class AddLectureUseCase
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IMapper _mapper;

        public AddLectureUseCase(
            ILectureRepository lectureRepository,
            IDisciplineDetailRepository disciplineDetailRepository,
            IMapper mapper)
        {
            _lectureRepository = lectureRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(LectureAddVM viewModel, ModelStateDictionary modelState)
        {
            var existingLecturesCount = await _lectureRepository.GetLectureCountByDisciplineDetailIdAsync(viewModel.DisciplineDetailId);
            var disciplineDetail =  _disciplineDetailRepository.GetDisciplineDetailByIdAsync(viewModel.DisciplineDetailId);
            int maxLectures = disciplineDetail.LectureCount;

            if (existingLecturesCount >= maxLectures)
            {
                modelState.AddModelError("", "Достигнуто максимальное количество лекций.");
                return false;
            }

            var lectureDTO = _mapper.Map<LectureDTO>(viewModel);
            await _lectureRepository.AddLectureAsync(lectureDTO);

            return true;
        }
    }
}
