using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureVisitRepository
    {
        Task AddLectureVisitAsync(LectureVisitDTO lectureVisitDTO);
        Task DeleteLectureVisit(int id);
        List<LectureVisitDTO> GetAllLectureVisits();
        LectureVisitDTO GetLectureVisitById(int id);
        void UpdateLectureVisit(LectureVisitDTO lectureVisitDTO);
    }
}