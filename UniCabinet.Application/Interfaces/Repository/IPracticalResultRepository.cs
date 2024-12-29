using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Application.Interfaces.Repository;

public interface IPracticalResultRepository
{
   
    Task AddOrUpdatePracticalResultAsync(PracticalResultDTO practicalResultDTO);
    Task<List<PracticalResultDTO>> GetPracticalResultsByPracticalIdAsync(int practicalId);
}
