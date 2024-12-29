using UniCabinet.Core.DTOs.LectureManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureVisitRepository
    {
      
        Task AddOrUpdateLectureVisitsAsync(List<LectureVisitDTO> lectureVisitDTO);
        Task<List<LectureVisitDTO>> GetLectureVisitsByLectureIdAsync(int lectureId);
    }
}
