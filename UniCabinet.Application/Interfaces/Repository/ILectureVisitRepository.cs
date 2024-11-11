using UniCabinet.Core.DTOs;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureVisitRepository
    {
        Task AddLectureVisitAsync(LectureVisitDTO lectureVisitDTO);
        Task DeleteLectureVisitAsync(int id);
        Task<List<LectureVisitDTO>> GetAllLectureVisitsAsync();
        Task<LectureVisitDTO> GetLectureVisitByIdAsync(int id);
        Task UpdateLectureVisitAsync(LectureVisitDTO lectureVisitDTO);
        Task AddOrUpdateLectureVisitAsync(LectureVisitDTO lectureVisitDTO);
        Task<List<LectureVisitDTO>> GetLectureVisitsByLectureIdAsync(int lectureId);
    }
}
