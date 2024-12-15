using UniCabinet.Core.DTOs.PracticalManagement;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Interfaces.Repository;

public interface IPracticalRepository
{
    Task<PracticalDTO> GetPracticalByIdAsync(int id);
    Task<List<PracticalDTO>> GetPracticalListByDisciplineDetailIdAsync(int id);
    Task<List<PracticalDTO>> GetAllPracticalsAsync();
    Task AddPracticalAsync(PracticalDTO practicalDTO);
    Task DeletePracticalAsync(int id);
    Task UpdatePracticalAsync(PracticalDTO practicalDTO);
    Task<int> GetPracticalCountByDisciplineDetailIdAsync(int disciplineDetailId);
    Task<List<PracticalEntity>> GetUnprocessedPracticalsForDateAsync(DateTime date);
    Task SaveChangesAsync();
}
