using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IDisciplineDetailRepository
    {
        void AddDisciplineDetail(DisciplineDetailDTO disciplineDetailDTO);
        void DeleteDisciplineDetail(int id);
        List<DisciplineDetailDTO> GetAllDisciplineDetails();
        Task<DisciplineDetailDTO> GetDisciplineDetailByIdAsync(int id);
        void UpdateDisciplineDetail(DisciplineDetailDTO disciplineDetailDTO);
        Task<DisciplineDetailDTO> GetDetailByUserAndDisciplineAsync(string userId, int disciplineId);

    }
}
