using Microsoft.EntityFrameworkCore;
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

        public async Task<List<SemesterDTO>> GetAllSemesters()
        {
            var semesterEntity = await _context.Semesters.ToListAsync();

            return semesterEntity.Select(d => new SemesterDTO
            {
                Id = d.Id,
                DayStart = d.DayStart,
                DayEnd = d.DayEnd,
                MounthStart = d.MounthStart,
                MounthEnd = d.MounthEnd,
                Number = d.Number,
            }).ToList();
        }

        public async Task<SemesterDTO> GetSemesterById(int id)
        {
            var semesterEntity = await _context.Semesters.FindAsync(id);
            if (semesterEntity == null) return null;

            return new SemesterDTO
            {
                DayStart = semesterEntity.DayStart,
                DayEnd = semesterEntity.DayEnd,
                MounthStart = semesterEntity.MounthStart,
                MounthEnd = semesterEntity.MounthEnd,
                Number = semesterEntity.Number,
            };
        }
    }
}
