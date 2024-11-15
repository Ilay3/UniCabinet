using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.LectureManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class LectureVisitRepositoryImpl : ILectureVisitRepository
    {
        private readonly ApplicationDbContext _context;

        public LectureVisitRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LectureVisitDTO> GetLectureVisitByIdAsync(int id)
        {
            var lectureVisitEntity = await _context.LectureVisits.FindAsync(id);
            if (lectureVisitEntity == null) return null;

            return new LectureVisitDTO
            {
                Id = lectureVisitEntity.Id,
                LectureId = lectureVisitEntity.LectureId,
                StudentId = lectureVisitEntity.StudentId,
                IsVisit = lectureVisitEntity.IsVisit,
                PointsCount = lectureVisitEntity.PointsCount
            };
        }

        public async Task<List<LectureVisitDTO>> GetAllLectureVisitsAsync()
        {
            var lectureVisitEntities = await _context.LectureVisits
                .Include(lv => lv.Lecture)
                .Include(lv => lv.Student)
                .AsNoTracking()
                .ToListAsync();

            return lectureVisitEntities.Select(d => new LectureVisitDTO
            {
                Id = d.Id,
                IsVisit = d.IsVisit,
                LectureId = d.LectureId,
                LectureNumber = d.Lecture.Number,
                StudentId = d.StudentId,
                SudentFirstName = d.Student.FirstName,
                StudentLastName = d.Student.LastName,
                StudentPatronymic = d.Student.Patronymic,
                PointsCount = d.PointsCount,
            }).ToList();
        }

        public async Task AddLectureVisitAsync(LectureVisitDTO lectureVisitDTO)
        {
            var lectureVisitEntity = new LectureVisitEntity
            {
                IsVisit = lectureVisitDTO.IsVisit,
                LectureId = lectureVisitDTO.LectureId,
                StudentId = lectureVisitDTO.StudentId,
                PointsCount = lectureVisitDTO.PointsCount
            };

            await _context.LectureVisits.AddAsync(lectureVisitEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLectureVisitAsync(int id)
        {
            var lectureVisitEntity = await _context.LectureVisits.FindAsync(id);
            if (lectureVisitEntity != null)
            {
                _context.LectureVisits.Remove(lectureVisitEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateLectureVisitAsync(LectureVisitDTO lectureVisitDTO)
        {
            var lectureVisitEntity = await _context.LectureVisits.FirstOrDefaultAsync(d => d.Id == lectureVisitDTO.Id);
            if (lectureVisitEntity == null) return;

            lectureVisitEntity.IsVisit = lectureVisitDTO.IsVisit;
            lectureVisitEntity.LectureId = lectureVisitDTO.LectureId;
            lectureVisitEntity.StudentId = lectureVisitDTO.StudentId;
            lectureVisitEntity.PointsCount = lectureVisitDTO.PointsCount;

            _context.LectureVisits.Update(lectureVisitEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrUpdateLectureVisitAsync(LectureVisitDTO lectureVisitDTO)
        {
            var existingVisit = await _context.LectureVisits
                .FirstOrDefaultAsync(lv => lv.LectureId == lectureVisitDTO.LectureId && lv.StudentId == lectureVisitDTO.StudentId);

            if (existingVisit != null)
            {
                existingVisit.IsVisit = lectureVisitDTO.IsVisit;
                existingVisit.PointsCount = lectureVisitDTO.PointsCount;
                _context.LectureVisits.Update(existingVisit);
            }
            else
            {
                var lectureVisitEntity = new LectureVisitEntity
                {
                    LectureId = lectureVisitDTO.LectureId,
                    StudentId = lectureVisitDTO.StudentId,
                    IsVisit = lectureVisitDTO.IsVisit,
                    PointsCount = lectureVisitDTO.PointsCount
                };
                await _context.LectureVisits.AddAsync(lectureVisitEntity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<LectureVisitDTO>> GetLectureVisitsByLectureIdAsync(int lectureId)
        {
            var lectureVisitEntities = await _context.LectureVisits
                .Where(lv => lv.LectureId == lectureId)
                .Include(lv => lv.Student)
                .AsNoTracking()
                .ToListAsync();

            return lectureVisitEntities.Select(d => new LectureVisitDTO
            {
                Id = d.Id,
                IsVisit = d.IsVisit,
                LectureId = d.LectureId,
                StudentId = d.StudentId,
                SudentFirstName = d.Student.FirstName,
                StudentLastName = d.Student.LastName,
                StudentPatronymic = d.Student.Patronymic,
                PointsCount = d.PointsCount,
            }).ToList();
        }
    }
}
