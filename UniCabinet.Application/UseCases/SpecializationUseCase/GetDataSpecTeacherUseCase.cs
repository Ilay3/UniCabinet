using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.SpecializationManagement;

namespace UniCabinet.Application.UseCases.SpecializationUseCase
{
    public class GetDataSpecTeacherUseCase
    {
        private readonly ISpecializationRepository _repository;

        public GetDataSpecTeacherUseCase(ISpecializationRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserSpecialtiesAndDisciplinesDTO> ExecuteAsync(string id)
        {
            return await _repository.GetSpecializationByTeacherId(id);
        }
    }
}
