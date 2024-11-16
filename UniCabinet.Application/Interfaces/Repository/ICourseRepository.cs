using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ICourseRepository
    {
        Task<List<CourseDTO>> GetAllCourseAsync();
        Task<CourseDTO> GetCourseById(int id);
    }
}