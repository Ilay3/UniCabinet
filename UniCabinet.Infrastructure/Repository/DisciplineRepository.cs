using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.Repository
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly ApplicationDbContext _context;

        public DisciplineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DisciplineDTO GetDisciplineById(int id)
        {
            var disciplineEntity = _context.Disciplines.Find(id);
            if (disciplineEntity == null) return null;

            return new DisciplineDTO
            {
                Name = disciplineEntity.Name,
                Description = disciplineEntity.Description,
            };
        }

        public List<DisciplineDTO> GetAllDisciplines()
        {
            var disciplineEntity = _context.Disciplines.ToList();

            return disciplineEntity.Select(d => new DisciplineDTO
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
            }).ToList();
        }

        public async Task AddDisciplineAsync(DisciplineDTO disciplineDTO)
        {
            var disciplineEntity = new Discipline
            {
                Name = disciplineDTO.Name,
                Description = disciplineDTO.Description,
            };

            await _context.Disciplines.AddAsync(disciplineEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDiscipline(int id)
        {
            var disciplineEntity = await _context.Disciplines.FindAsync(id);
            if (disciplineEntity != null)
            {
                _context.Disciplines.Remove(disciplineEntity);
                await _context.SaveChangesAsync();
            }
        }

        public void UpdateDiscipline(DisciplineDTO disciplineDTO)
        {
            var disciplineEntity = _context.Disciplines.FirstOrDefault(d => d.Id == disciplineDTO.Id);
            if (disciplineEntity == null) return;

            disciplineEntity.Name = disciplineDTO.Name;
            disciplineEntity.Description = disciplineDTO.Description;

            _context.Disciplines.Update(disciplineEntity);
            _context.SaveChanges();
        }
    }
}
