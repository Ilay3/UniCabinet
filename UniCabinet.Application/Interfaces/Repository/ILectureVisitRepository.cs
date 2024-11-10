using UniCabinet.Core.DTOs;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureVisitRepository
    {
        void AddLectureVisit(LectureVisitDTO lectureVisitDTO);
        void DeleteLectureVisit(int id);
        List<LectureVisitDTO> GetAllLectureVisits();
        LectureVisitDTO GetLectureVisitById(int id);
        void UpdateLectureVisit(LectureVisitDTO lectureVisitDTO);
        void AddOrUpdateLectureVisit(LectureVisitDTO lectureVisitDTO);
        List<LectureVisitDTO> GetLectureVisitsByLectureId(int lectureId);
    }
}