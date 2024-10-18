using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IDisciplineRepository
    {
        void AddDiscipline(DisciplineDTO disciplineDTO);
        void DeleteDiscipline(int id);
        List<DisciplineDTO> GetAllDisciplines();
        DisciplineDTO GetDisciplineById(int id);
        void UpdateDiscipline(DisciplineDTO disciplineDTO);
    }
}