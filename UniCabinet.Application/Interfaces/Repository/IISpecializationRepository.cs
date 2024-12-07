using UniCabinet.Core.DTOs.SpecializationManagement;

namespace UniCabinet.Application.Interfaces.Repository
{
    public interface ISpecializationRepository
    {
        Task<List<SpecializationDTO>> GetAllSpecialization();
        Task<List<SpecializationListDTO>> GetDataSpecializationAndTeacher();
        Task<SpecializationDTO> GetSpecializationById(int id);
        Task AddSpecialization(SpecializationAddDTO specializationDTO);
        Task UpdateSpecialization(SpecializationEditDTO specializationDTO);
        Task<UserSpecialtiesAndDisciplinesDTO> GetSpecializationByTeacherId(string teachid);
    }

}
