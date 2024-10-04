using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureRepository
    {
        Task AddLectureAsync(LectureDTO lectureDTO);
        Task DeleteLecture(int id);
        List<LectureDTO> GetAllLectures();
        LectureDTO GetLectureById(int id);
        void UpdateLecture(LectureDTO lectureDTO);
    }
}