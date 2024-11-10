using UniCabinet.Core.DTOs.Entites;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureRepository
    {
        void AddLecture(LectureDTO lectureDTO);
        Task<List<LectureDTO>> GetLectureListByDisciplineDetailId(int id);
        Task DeleteLecture(int id);
        List<LectureDTO> GetAllLectures();
        LectureDTO GetLectureById(int id);
        void UpdateLecture(LectureDTO lectureDTO);
        int GetLectureCountByDisciplineDetailId(int disciplineDetailId);
    }
}