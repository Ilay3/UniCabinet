using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class ExamResultRepository : IExamResultRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ExamResultDTO GetExamResultById(int id)
        {
            var examResultEntity = _context.ExamResults.Find(id);
            if (examResultEntity == null) return null;

            return new ExamResultDTO
            {
                ExamId = examResultEntity.ExamId,
                FinalPoint = examResultEntity.FinalPoint,
                IsAutomatic = examResultEntity.IsAutomatic,
                PointAvarage = examResultEntity.PointAvarage,
                StudentId = examResultEntity.StudentId,
            };
        }

        public List<ExamResultDTO> GetAllExamResults()
        {
            var examResultEntity = _context.ExamResults.ToList();

            return examResultEntity.Select(d => new ExamResultDTO
            {
                Id = d.Id,
                ExamId = d.ExamId,
                FinalPoint = d.FinalPoint,
                StudentId = d.StudentId,
                IsAutomatic = d.IsAutomatic,
                PointAvarage = d.PointAvarage,
            }).ToList();
        }

        public async Task AddExamResultAsync(ExamResultDTO examResultDTO)
        {
            var examResultEntity = new ExamResult
            {
                ExamId = examResultDTO.ExamId,
                StudentId = examResultDTO.StudentId,
                PointAvarage = examResultDTO.PointAvarage,
                FinalPoint = examResultDTO.FinalPoint,
                IsAutomatic = examResultDTO.IsAutomatic,
            };

            await _context.ExamResults.AddAsync(examResultEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExamResult(int id)
        {
            var examResultEntity = await _context.ExamResults.FindAsync(id);
            if (examResultEntity != null)
            {
                _context.ExamResults.Remove(examResultEntity);
                await _context.SaveChangesAsync();
            }
        }

        public void UpdateExamResult(ExamResultDTO examResultDTO)
        {
            var examResultEntity = _context.ExamResults.FirstOrDefault(d => d.Id == examResultDTO.Id);
            if (examResultEntity == null) return;

            examResultEntity.FinalPoint = examResultDTO.FinalPoint;
            examResultEntity.PointAvarage = examResultDTO.PointAvarage;
            examResultEntity.IsAutomatic = examResultDTO.IsAutomatic;
            examResultEntity.ExamId = examResultDTO.ExamId;
            examResultEntity.StudentId = examResultDTO.StudentId;

            _context.SaveChanges();
        }
    }
}
