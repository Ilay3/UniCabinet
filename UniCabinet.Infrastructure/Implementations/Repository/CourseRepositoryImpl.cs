using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Infrastructure.Data;


namespace UniCabinet.Infrastructure.Implementations.Repository;

public class CourseRepositoryImpl : ICourseRepository
{
    private readonly ApplicationDbContext _context;
    public CourseRepositoryImpl(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<CourseDTO>> GetAllCourseAsync()
    {
        var courseEntities = await _context.Courses.ToListAsync();

        return courseEntities.Select(d => new CourseDTO
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
