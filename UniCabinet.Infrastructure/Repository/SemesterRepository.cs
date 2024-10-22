using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UniCabinet.Infrastructure.Repository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly ApplicationDbContext _context;

        public SemesterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SemesterDTO>> GetAllSemestersAsync()
        {
            var semesterEntities = await _context.Semesters.ToListAsync();

            return semesterEntities.Select(d => new SemesterDTO
            {
                Id = d.Id,
                DayStart = d.DayStart,
                DayEnd = d.DayEnd,
                MounthStart = d.MounthStart,
                MounthEnd = d.MounthEnd,
                Number = d.Number,
            }).ToList();
        }

        public async Task<SemesterDTO> GetSemesterByIdAsync(int id)
        {
            var semesterEntity = await _context.Semesters.FindAsync(id);
            if (semesterEntity == null) return null;

            return new SemesterDTO
            {
                Id = semesterEntity.Id,
                DayStart = semesterEntity.DayStart,
                DayEnd = semesterEntity.DayEnd,
                MounthStart = semesterEntity.MounthStart,
                MounthEnd = semesterEntity.MounthEnd,
                Number = semesterEntity.Number,
            };
        }

        public async Task<Semester> GetSemesterEntityByIdAsync(int id)
        {
            return await _context.Semesters.FindAsync(id);
        }

        public async Task<SemesterDTO> GetCurrentSemesterAsync(DateTime currentDate)
        {
            // Логика определения текущего семестра
            var semesterEntity = await _context.Semesters
                .FirstOrDefaultAsync(s =>
                    (currentDate.Month > s.MounthStart || (currentDate.Month == s.MounthStart && currentDate.Day >= s.DayStart)) &&
                    (currentDate.Month < s.MounthEnd || (currentDate.Month == s.MounthEnd && currentDate.Day <= s.DayEnd))
                );

            if (semesterEntity == null)
            {
                throw new InvalidOperationException("Текущий семестр не найден.");
            }

            return new SemesterDTO
            {
                Id = semesterEntity.Id,
                Number = semesterEntity.Number,
                DayStart = semesterEntity.DayStart,
                MounthStart = semesterEntity.MounthStart,
                DayEnd = semesterEntity.DayEnd,
                MounthEnd = semesterEntity.MounthEnd,
            };
        }

        public void Add(Semester semester)
        {
            _context.Semesters.Add(semester);
        }

        public void Update(Semester semester)
        {
            _context.Semesters.Update(semester);
        }

        public void Remove(Semester semester)
        {
            _context.Semesters.Remove(semester);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
