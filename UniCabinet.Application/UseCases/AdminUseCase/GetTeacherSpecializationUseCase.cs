using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.SpecializationManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class GetTeacherSpecializationUseCase
    {
        private readonly ISpecializationRepository _specializationRepository;
        public GetTeacherSpecializationUseCase(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }
        public async Task<List<SpecializationListDTO>> ExecuteAsync()
        {
            var spec = await _specializationRepository.GetDataSpecializationAndTeacher();
            return spec;

        }
    }
}
