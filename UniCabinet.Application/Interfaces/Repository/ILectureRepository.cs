using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureRepository
    {
        void AddLecture(LectureDTO lectureDTO);
        IEnumerable<LectureDTO> GetLectureListByDisciplineDetailId(int id);
        Task DeleteLecture(int id);
        List<LectureDTO> GetAllLectures();
        LectureDTO GetLectureById(int id);
        void UpdateLecture(LectureDTO lectureDTO);
    }
}