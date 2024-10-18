using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ICourseRepository
    {
        List<CourseDTO> GetAllCourse();
        CourseDTO GetCourseById(int id);
    }
}