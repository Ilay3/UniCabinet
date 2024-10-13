using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class LectureRepository : ILectureRepository
    {
        private readonly ApplicationDbContext _context;
        public LectureRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LectureDTO> GetLectureById(int id)
        {
            var lectureEntity = await _context.Lectures.FindAsync(id);
            if (lectureEntity == null) return null;

            return new LectureDTO
            {
                Date = lectureEntity.Date,
                DisciplineDetailId = lectureEntity.DisciplineDetailId,
                LectureNumber = lectureEntity.LectureNumber,
            };
        }

        public async Task<IEnumerable<LectureDTO>> GetLectureListByDisciplineDetailId(int id)
        {
            var lectureListEntity = await _context.Lectures
                .Where(l => l.DisciplineDetailId == id)
                .ToListAsync();

            var disciplineDetailEntity = await _context.DisciplineDetails.FindAsync(id);
            var disciplineEntity = await _context.Disciplines.FindAsync(disciplineDetailEntity.DisciplineId);

            return lectureListEntity
                .Select(l => new LectureDTO
                {
                    Date = l.Date,
                    DisciplineDetailId = l.DisciplineDetailId,
                    LectureNumber= l.LectureNumber,
                }).ToList();
        }

        public List<LectureDTO> GetAllLectures()
        {
            var lectureEntity = _context.Lectures.ToList();

            return lectureEntity.Select(d => new LectureDTO
            {
                Id = d.Id,
                Date = d.Date,
                DisciplineDetailId = d.DisciplineDetailId,
                LectureNumber = d.LectureNumber,
            }).ToList();
        }

        public async Task AddLectureAsync(LectureDTO lectureDTO)
        {
            var lectureEntity = new Lecture
            {
                Date = lectureDTO.Date,
                DisciplineDetailId = lectureDTO.DisciplineDetailId,
                LectureNumber = lectureDTO.LectureNumber,
            };

            await _context.Lectures.AddAsync(lectureEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLecture(int id)
        {
            var lectureEntity = await _context.Lectures.FindAsync(id);
            if (lectureEntity != null)
            {
                _context.Lectures.Remove(lectureEntity);
                await _context.SaveChangesAsync();
            }
        }

        public void UpdateLecture(LectureDTO lectureDTO)
        {
            var lectureEntity = _context.Lectures.FirstOrDefault(d => d.Id == lectureDTO.Id);
            if (lectureEntity == null) return;

            lectureEntity.LectureNumber = lectureDTO.LectureNumber;
            lectureEntity.DisciplineDetailId = lectureDTO.DisciplineDetailId;
            lectureEntity.Date = lectureDTO.Date;

            _context.SaveChanges();
        }
    }
}
