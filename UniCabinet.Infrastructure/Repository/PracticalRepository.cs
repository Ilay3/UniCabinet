using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class PracticalRepository : IPracticalRepository
    {
        private readonly ApplicationDbContext _context;
        public PracticalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PracticalDTO GetPracticalById(int id)
        {
            var practicalEntity = _context.Practicals.Find(id);
            if (practicalEntity == null) return null;

            return new PracticalDTO
            {
                Date = practicalEntity.Date,
                DisciplineDetailId = practicalEntity.DisciplineDetailId,
                PracticalNumber = practicalEntity.PracticalNumber,
            };
        }

        public List<PracticalDTO> GetAllPracticals()
        {
            var practicalEntity = _context.Practicals.ToList();

            return practicalEntity.Select(d => new PracticalDTO
            {
                Id = d.Id,
                Date = d.Date,
                DisciplineDetailId = d.DisciplineDetailId,
                PracticalNumber = d.PracticalNumber,
            }).ToList();
        }

        public async Task AddPracticalAsync(PracticalDTO practicalDTO)
        {
            var practicalEntity = new Practical
            {
                Date = practicalDTO.Date,
                DisciplineDetailId = practicalDTO.DisciplineDetailId,
                PracticalNumber = practicalDTO.PracticalNumber,
            };

            await _context.Practicals.AddAsync(practicalEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePractical(int id)
        {
            var practicalEntity = await _context.Practicals.FindAsync(id);
            if (practicalEntity != null)
            {
                _context.Practicals.Remove(practicalEntity);
                await _context.SaveChangesAsync();
            }
        }

        public void UpdatePractical(PracticalDTO practicalDTO)
        {
            var practicalEntity = _context.Practicals.FirstOrDefault(d => d.Id == practicalDTO.Id);
            if (practicalEntity == null) return;

            practicalEntity.PracticalNumber = practicalDTO.PracticalNumber;
            practicalEntity.Date = practicalDTO.Date;
            practicalEntity.DisciplineDetailId = practicalDTO.DisciplineDetailId;

            _context.SaveChanges();
        }
    }
}
