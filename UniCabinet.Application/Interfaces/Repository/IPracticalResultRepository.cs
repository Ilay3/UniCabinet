using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IPracticalResultRepository
    {
        void AddPracticalResult(PracticalResultDTO practicalResultDTO);
        void DeletePracticalResult(int id);
        List<PracticalResultDTO> GetAllPracticalResults();
        PracticalResultDTO GetPracticalResultById(int id);
        void UpdatePracticalResult(PracticalResultDTO practicalResultDTO);
    }
}