using UniCabinet.Core.DTOs.Entites;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ILectureRepository
    {
        Task AddLectureAsync(LectureDTO lectureDTO);
        Task<List<LectureDTO>> GetLectureListByDisciplineDetailIdAsync(int id);
        Task DeleteLectureAsync(int id);
        Task<List<LectureDTO>> GetAllLecturesAsync();
        Task<LectureDTO> GetLectureByIdAsync(int id);
        Task UpdateLectureAsync(LectureDTO lectureDTO);
        Task<int> GetLectureCountByDisciplineDetailIdAsync(int disciplineDetailId);
    }

}