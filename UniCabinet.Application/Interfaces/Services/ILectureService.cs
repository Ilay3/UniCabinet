namespace UniCabinet.Application.Interfaces.Services
{
    public interface ILectureService
    {
        Task<string> GetDisciplineById(int id);
    }
}