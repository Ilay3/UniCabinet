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

        public List<CourseDTO> GetAllCourse()
        {
            var courseEntity = _context.Courses.ToList();

            return courseEntity.Select(d => new CourseDTO
            {
                Id = d.Id,
                Number = d.Number,
            }).ToList();
        }

        public CourseDTO GetCourseById(int id)
        {
            var courseEntity = _context.Courses.Find(id);
            if (courseEntity == null) return null;

            return new CourseDTO
            {
                Number = courseEntity.Number,
            };
        }
    }
}
