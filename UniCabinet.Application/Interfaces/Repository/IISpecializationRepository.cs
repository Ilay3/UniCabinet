using UniCabinet.Core.DTOs.SpecializationManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISpecializationRepository
    {
        Task<List<SpecializationDTO>> GetAllSpecialization();
        Task<List<SpecializationDTO>> GetDataSpecializationAndTeacher();

    }
}
