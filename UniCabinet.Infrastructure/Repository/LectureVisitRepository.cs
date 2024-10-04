using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class LectureVisitRepository : ILectureVisitRepository
    {
        private readonly ApplicationDbContext _context;
        public LectureVisitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public LectureVisitDTO GetLectureVisitById(int id)
        {
            var lectureVisitEntity = _context.LectureVisits.Find(id);
            if (lectureVisitEntity == null) return null;

            return new LectureVisitDTO
            {
                LectureId = lectureVisitEntity.LectureId,
                PointsCount = lectureVisitEntity.PointsCount,
                StudentId = lectureVisitEntity.StudentId,
                isVisit = lectureVisitEntity.IsVisit,
            };
        }

        public List<LectureVisitDTO> GetAllLectureVisits()
        {
            var lectureVisitEntity = _context.LectureVisits.ToList();

            return lectureVisitEntity.Select(d => new LectureVisitDTO
            {
                Id = d.Id,
                isVisit = d.IsVisit,
                LectureId = d.LectureId,
                StudentId = d.StudentId,
                PointsCount = d.PointsCount,
            }).ToList();
        }

        public async Task AddLectureVisitAsync(LectureVisitDTO lectureVisitDTO)
        {
            var lectureVisitEntity = new LectureVisit
            {
                IsVisit = lectureVisitDTO.isVisit,
                LectureId = lectureVisitDTO.LectureId,
                PointsCount = lectureVisitDTO.PointsCount,
                StudentId = lectureVisitDTO.StudentId,
            };

            await _context.LectureVisits.AddAsync(lectureVisitEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLectureVisit(int id)
        {
            var lectureVisitEntity = await _context.LectureVisits.FindAsync(id);
            if (lectureVisitEntity != null)
            {
                _context.LectureVisits.Remove(lectureVisitEntity);
                await _context.SaveChangesAsync();
            }
        }

        public void UpdateLectureVisit(LectureVisitDTO lectureVisitDTO)
        {
            var lectureVisitEntity = _context.LectureVisits.FirstOrDefault(d => d.Id == lectureVisitDTO.Id);
            if (lectureVisitEntity == null) return;

            lectureVisitEntity.IsVisit = lectureVisitDTO.isVisit;
            lectureVisitEntity.LectureId = lectureVisitDTO.LectureId;
            lectureVisitEntity.StudentId = lectureVisitDTO.StudentId;
            lectureVisitEntity.PointsCount = lectureVisitDTO.PointsCount;

            _context.SaveChanges();
        }
    }
}
