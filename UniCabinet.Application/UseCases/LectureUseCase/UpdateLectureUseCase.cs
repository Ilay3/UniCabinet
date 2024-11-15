using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.LectureManagement;
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

        public async Task ExecuteAsync(LectureDTO lectureDTO)
        {
            await _lectureRepository.UpdateLectureAsync(lectureDTO);
        }
    }
}
