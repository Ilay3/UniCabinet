using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IPracticalResultRepository
    {
        Task AddPracticalResultAsync(PracticalResultDTO practicalResultDTO);
        Task DeletePracticalResult(int id);
        List<PracticalResultDTO> GetAllPracticalResults();
        PracticalResultDTO GetPracticalResultById(int id);
        void UpdatePracticalResult(PracticalResultDTO practicalResultDTO);
    }
}