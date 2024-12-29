using Microsoft.EntityFrameworkCore;
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



        public async Task AddOrUpdateLectureVisitsAsync(List<LectureVisitDTO> lectureVisitDTOs)
        {
            try
            {
                if (lectureVisitDTOs == null || !lectureVisitDTOs.Any())
                    return;

                var lectureIds = lectureVisitDTOs.Select(dto => dto.LectureId).Distinct();
                var studentIds = lectureVisitDTOs.Select(dto => dto.StudentId).Distinct();

                var existingVisits = await _context.LectureVisits
                    .Where(lv => lectureIds.Contains(lv.LectureId))
                    .Where(lv => studentIds.Contains(lv.StudentId))
                    .ToListAsync();


                var visitsToUpdate = new List<LectureVisitEntity>();
                var visitsToAdd = new List<LectureVisitEntity>();

                foreach (var dto in lectureVisitDTOs)
                {
                    var existingVisit = existingVisits.FirstOrDefault(lv => lv.LectureId == dto.LectureId && lv.StudentId == dto.StudentId);
                    if (existingVisit != null)
                    {
                        existingVisit.IsVisit = dto.IsVisit;
                        existingVisit.PointsCount = dto.PointsCount;
                        visitsToUpdate.Add(existingVisit);
                    }
                    else
                    {
                        var newVisit = new LectureVisitEntity
                        {
                            LectureId = dto.LectureId,
                            StudentId = dto.StudentId,
                            IsVisit = dto.IsVisit,
                            PointsCount = dto.PointsCount
                        };
                        visitsToAdd.Add(newVisit);
                    }
                }

                if (visitsToUpdate.Any())
                {
                    _context.LectureVisits.UpdateRange(visitsToUpdate);
                }

                if (visitsToAdd.Any())
                {
                    await _context.LectureVisits.AddRangeAsync(visitsToAdd);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
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
