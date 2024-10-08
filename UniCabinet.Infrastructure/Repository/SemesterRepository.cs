using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly ApplicationDbContext _context;
        public SemesterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SemesterDTO> GetAllSemesters()
        {
            var semesterEntity = _context.Semesters.ToList();

            return semesterEntity.Select(d => new SemesterDTO
            {
                Id = d.Id,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                CourseId = d.CourseId,
                Number = d.Number,
            }).ToList();
        }

        public SemesterDTO GetSemesterById(int id)
        {
            var semesterEntity = _context.Semesters.Find(id);
            if (semesterEntity == null) return null;

            return new SemesterDTO
            {
                StartDate = semesterEntity.StartDate,
                EndDate = semesterEntity.EndDate,
                CourseId = semesterEntity.CourseId,
                Number = semesterEntity.Number,
            };
        }
    }
}
