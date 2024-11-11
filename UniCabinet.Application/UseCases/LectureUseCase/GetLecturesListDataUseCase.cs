using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs;

namespace UniCabinet.Application.UseCases.LectureUseCase
{
    public class GetLecturesListDataUseCase
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        public GetLecturesListDataUseCase(ILectureRepository lectureRepository,
            IDisciplineDetailRepository disciplineDetailRepository,
            IDisciplineRepository disciplineRepository)
        {
            _lectureRepository = lectureRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _disciplineRepository = disciplineRepository;
        }


        public async Task<LectureListDTO> ExecuteAsync(int id)
        {
            var lectureListDTO = await _lectureRepository.GetLectureListByDisciplineDetailIdAsync(id);
            var disciplineDetail = await _disciplineDetailRepository.GetDisciplineDetailByIdAsync(id);

            var disciplineDetailDTO = await _disciplineDetailRepository.GetDisciplineDetailByIdAsync(id);
            var disciplineDTO = await _disciplineRepository.GetDisciplineByIdAsync(disciplineDetailDTO.DisciplineId);
            var result = new LectureListDTO
            {
                DisciplineName = disciplineDTO.Name,
                MaxLectures = disciplineDetail.LectureCount,
                LectureDTO = lectureListDTO
            };
            return result;
        }
    }
}
