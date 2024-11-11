using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Lecture;

namespace UniCabinet.Application.UseCases.LectureUseCase
{
    public class UpdateLectureUseCase
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IMapper _mapper;

        public UpdateLectureUseCase(
            ILectureRepository lectureRepository,
            IMapper mapper)
        {
            _lectureRepository = lectureRepository;
            _mapper = mapper;
        }

        public void Execute(LectureEditVM viewModel)
        {
            var lectureDTO = _mapper.Map<LectureDTO>(viewModel);
            _lectureRepository.UpdateLectureAsync(lectureDTO);
        }
    }
}
