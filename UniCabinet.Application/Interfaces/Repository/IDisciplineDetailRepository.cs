using UniCabinet.Core.DTOs.Entites;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IDisciplineDetailRepository
    {
        void AddDisciplineDetail(DisciplineDetailDTO disciplineDetailDTO);
        void DeleteDisciplineDetail(int id);
        List<DisciplineDetailDTO> GetAllDisciplineDetails();
        DisciplineDetailDTO GetDisciplineDetailById(int id);
        void UpdateDisciplineDetail(DisciplineDetailDTO disciplineDetailDTO);
    }
}
