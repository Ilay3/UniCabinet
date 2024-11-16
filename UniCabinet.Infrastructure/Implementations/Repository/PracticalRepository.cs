using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
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

        public void AddPractical(PracticalDTO practicalDTO)
        {
            var practicalEntity = new PracticalEntity
            {
                Date = practicalDTO.Date,
                DisciplineDetailId = practicalDTO.DisciplineDetailId,
                PracticalNumber = practicalDTO.PracticalNumber,
            };

            _context.Practicals.Find(practicalEntity);
            _context.SaveChanges();
        }

        public void DeletePractical(int id)
        {
            var practicalEntity = _context.Practicals.Find(id);
            if (practicalEntity != null)
            {
                _context.Practicals.Remove(practicalEntity);
                _context.SaveChanges();
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
