using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IDisciplineDetailRepository
    {
        Task AddDisciplineDetailAsync(DisciplineDetailDTO disciplineDetailDTO);
        Task DeleteDisciplineDetail(int id);
        List<DisciplineDetailDTO> GetAllDisciplineDetails();
        Task<DisciplineDetailDTO> GetDisciplineDetailById(int id);
        void UpdateDisciplineDetail(DisciplineDetailDTO disciplineDetailDTO);
    }
}