using UniCabinet.Core.DTOs.LectureManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureRepository
    {
        Task AddLectureAsync(LectureDTO lectureDTO);
        Task<List<LectureDTO>> GetLectureListByDisciplineDetailIdAsync(int id);

        Task<LectureDTO> GetLectureByIdAsync(int id);
        Task UpdateLectureAsync(LectureDTO lectureDTO);
        Task<int> GetLectureCountByDisciplineDetailIdAsync(int disciplineDetailId);
        Task<List<LectureEntity>> GetUnprocessedLecturesForDateAsync(DateTime date);
        Task SaveChangesAsync();
    }

}