using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class PracticalResultRepository : IPracticalResultRepository
    {
        private readonly ApplicationDbContext _context;
        public PracticalResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public PracticalResultDTO GetPracticalResultById(int id)
        {
            var practicalResultEntity = _context.PracticalResults.Find(id);
            if (practicalResultEntity == null) return null;

            return new PracticalResultDTO
            {
                Point = practicalResultEntity.Point,
                Grade = practicalResultEntity.Grade,
                PracticalId = practicalResultEntity.PracticalId,
                StudentId = practicalResultEntity.StudentId,
            };
        }

        public List<PracticalResultDTO> GetAllPracticalResults()
        {
            var practicalResultEntity = _context.PracticalResults.ToList();

            return practicalResultEntity.Select(d => new PracticalResultDTO
            {
                Id = d.Id,
                StudentId = d.StudentId,
                Grade = d.Grade,
                Point = d.Point,
                PracticalId = d.PracticalId,
            }).ToList();
        }

        public void AddPracticalResult(PracticalResultDTO practicalResultDTO)
        {
            var practicalResultEntity = new PracticalResult
            {
                Grade = practicalResultDTO.Grade,
                Point = practicalResultDTO.Point,
                PracticalId = practicalResultDTO.PracticalId,
                StudentId = practicalResultDTO.StudentId,
            };

            _context.PracticalResults.Find(practicalResultEntity);
            _context.SaveChanges();
        }

        public void DeletePracticalResult(int id)
        {
            var practicalResultEntity = _context.PracticalResults.Find(id);
            if (practicalResultEntity != null)
            {
                _context.PracticalResults.Remove(practicalResultEntity);
                _context.SaveChanges();
            }
        }

        public void UpdatePracticalResult(PracticalResultDTO practicalResultDTO)
        {
            var practicalResultEntity = _context.PracticalResults.FirstOrDefault(d => d.Id == practicalResultDTO.Id);
            if (practicalResultEntity == null) return;

            practicalResultEntity.Grade = practicalResultDTO.Grade;
            practicalResultEntity.Point = practicalResultDTO.Point;
            practicalResultEntity.PracticalId = practicalResultDTO.PracticalId;
            practicalResultEntity.StudentId = practicalResultDTO.StudentId;

            _context.SaveChanges();
        }
    }
}
