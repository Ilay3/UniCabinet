using UniCabinet.Core.DTOs.DisciplineDetailManagment;
using UniCabinet.Domain.Entities;

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

        Task<List<DisciplineDetailDTO>> GetByDisciplineAndTeacherAsync(int disciplineId, string teacherId);
        Task<List<DisciplineDetailDTO>> GetByDisciplineTeacherAndFiltersAsync(int disciplineId, string teacherId, int? courseId, int? semesterId, int? groupId);
        Task<DisciplineDetailDTO> GetByIdAsync(int detailId);

    }
}
