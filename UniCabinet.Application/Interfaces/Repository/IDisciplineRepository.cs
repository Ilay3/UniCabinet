using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface IDisciplineRepository
    {
        Task AddDisciplineAsync(DisciplineDTO disciplineDTO);
        Task DeleteDiscipline(int id);
        List<DisciplineDTO> GetAllDisciplines();
        DisciplineDTO GetDisciplineById(int id);
        void UpdateDiscipline(DisciplineDTO disciplineDTO);
    }
}