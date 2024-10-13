using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ICourseRepository
    {
        Task <List<CourseDTO>> GetAllCourse();
        Task<CourseDTO> GetCourseById(int id);
    }
}