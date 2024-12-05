using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Application.Interfaces.Repository;

public interface IPracticalResultRepository
{
    Task<PracticalResultDTO> GetPracticalResultByIdAsync(int id);
    Task<List<PracticalResultDTO>> GetAllPracticalResultsAsync();
    Task AddPracticalResultAsync(PracticalResultDTO practicalResultDTO);
    Task DeletePracticalResultAsync(int id);
    Task UpdatePracticalResultAsync(PracticalResultDTO practicalResultDTO);
    Task AddOrUpdatePracticalResultAsync(PracticalResultDTO practicalResultDTO);
    Task<List<PracticalResultDTO>> GetPracticalResultsByPracticalIdAsync(int practicalId);
}
