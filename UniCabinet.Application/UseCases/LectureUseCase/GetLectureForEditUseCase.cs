using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Lecture;

namespace UniCabinet.Application.UseCases.LectureUseCase
{
    public class GetLectureForEditUseCase
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IMapper _mapper;

        public GetLectureForEditUseCase(
            ILectureRepository lectureRepository,
            IMapper mapper)
        {
            _lectureRepository = lectureRepository;
            _mapper = mapper;
        }

        public async Task<LectureDTO> ExecuteAsync(int id)
        {
            var lectureDTO = await _lectureRepository.GetLectureByIdAsync(id);
            return lectureDTO;
        }
    }
}
