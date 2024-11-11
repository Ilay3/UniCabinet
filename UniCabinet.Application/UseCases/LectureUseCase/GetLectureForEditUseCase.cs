using AutoMapper;
using UniCabinet.Application.Interfaces.Repository;
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

        public LectureEditVM Execute(int id)
        {
            var lectureDTO = _lectureRepository.GetLectureByIdAsync(id);
            var lectureVM = _mapper.Map<LectureEditVM>(lectureDTO);
            return lectureVM;
        }
    }
}
