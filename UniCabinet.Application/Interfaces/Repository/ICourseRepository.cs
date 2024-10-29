using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ICourseRepository
    {
        Task<List<CourseDTO>> GetAllCourseAsync();
        CourseDTO GetCourseById(int id);
    }
}