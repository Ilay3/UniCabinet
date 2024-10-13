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
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseDTO>> GetAllCourse()
        {
            var courseEntity = await _context.Courses.ToListAsync();

            return courseEntity.Select(d => new CourseDTO
            {
                Id = d.Id,
                Number = d.Number,
            }).ToList();
        }

        public async Task<CourseDTO> GetCourseById(int id)
        {
            var courseEntity = await _context.Courses.FindAsync(id);
            if (courseEntity == null) return null;

            return new CourseDTO
            {
                Number = courseEntity.Number,
            };
        }
    }
}
