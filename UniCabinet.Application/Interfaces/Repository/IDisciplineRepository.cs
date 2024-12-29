using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IDisciplineRepository
    {
        Task AddDisciplineAsync(DisciplineDTO disciplineDTO);
        Task DeleteDisciplineAsync(int id);
        Task<List<DisciplineDTO>> GetAllDisciplinesAsync();
        Task<DisciplineDTO> GetDisciplineByIdAsync(int id);
        Task UpdateDisciplineAsync(DisciplineDTO disciplineDTO);
        Task<List<DisciplineDTO>> GetDisciplinesBySpecialtyIdAsync(int? specialtyId);
        Task<List<DisciplineDTO>> GetDisciplinesByTeacherIdAsync(string teacherId);


    }
}
