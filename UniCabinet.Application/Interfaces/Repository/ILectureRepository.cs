using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureRepository
    {
        Task AddLectureAsync(LectureDTO lectureDTO);
        Task<IEnumerable<LectureDTO>> GetLectureListByDisciplineDetailId(int id);
        Task DeleteLecture(int id);
        List<LectureDTO> GetAllLectures();
        Task<LectureDTO> GetLectureById(int id);
        void UpdateLecture(LectureDTO lectureDTO);
    }
}