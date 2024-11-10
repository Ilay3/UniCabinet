using UniCabinet.Core.DTOs.Entites;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IPracticalRepository
    {
        void AddPractical(PracticalDTO practicalDTO);
        void DeletePractical(int id);
        List<PracticalDTO> GetAllPracticals();
        PracticalDTO GetPracticalById(int id);
        void UpdatePractical(PracticalDTO practicalDTO);
    }
}