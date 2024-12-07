using UniCabinet.Core.DTOs.DisciplineDetailManagment;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IDisciplineDetailRepository
    {

        List<DisciplineDetailDTO> GetAllDisciplineDetails();
        Task<DisciplineDetailDTO> GetDisciplineDetailByIdAsync(int id);
        Task<DisciplineDetailDTO> GetDetailByUserAndDisciplineAsync(string userId, int disciplineId);

        Task<List<DisciplineDetailDTO>> GetByDisciplineAndTeacherAsync(int disciplineId, string teacherId);
        Task<List<DisciplineDetailDTO>> GetByDisciplineTeacherAndFiltersAsync(int disciplineId, string teacherId, int? courseId, int? semesterId, int? groupId);
        Task<DisciplineDetailDTO> GetByIdAsync(int detailId);
        Task UpdateAsync(DisciplineDetailDTO dto);
        Task AddAsync(DisciplineDetailDTO disciplineDetailDTO);
    }
}
