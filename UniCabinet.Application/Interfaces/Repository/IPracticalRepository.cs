using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IPracticalRepository
    {
        Task AddPracticalAsync(PracticalDTO practicalDTO);
        Task DeletePractical(int id);
        List<PracticalDTO> GetAllPracticals();
        PracticalDTO GetPracticalById(int id);
        void UpdatePractical(PracticalDTO practicalDTO);
    }
}