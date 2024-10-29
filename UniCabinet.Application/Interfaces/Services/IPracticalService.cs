using System.Collections.Generic;
using System.Threading.Tasks;
using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Services
{
    public interface IPracticalService
    {
        Task<List<PracticalDTO>> GetPracticalsByDisciplineDetailIdAsync(int disciplineDetailId);
        Task<PracticalDTO> GetPracticalByIdAsync(int id);
        Task AddPracticalAsync(PracticalDTO practicalDTO);
        Task UpdatePracticalAsync(PracticalDTO practicalDTO);
        Task DeletePracticalAsync(int id);
    }
}
