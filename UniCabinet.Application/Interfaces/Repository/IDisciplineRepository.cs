using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Core.DTOs.Entites;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IDisciplineRepository
    {
        Task AddDisciplineAsync(DisciplineDTO disciplineDTO);
        Task DeleteDisciplineAsync(int id);
        Task<List<DisciplineDTO>> GetAllDisciplinesAsync();
        Task<DisciplineDTO> GetDisciplineByIdAsync(int id);
        Task UpdateDisciplineAsync(DisciplineDTO disciplineDTO);
    }
}
