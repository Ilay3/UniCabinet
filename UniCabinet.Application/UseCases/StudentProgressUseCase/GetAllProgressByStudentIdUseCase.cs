using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Core.DTOs.StudentProgressManagment;

namespace UniCabinet.Application.UseCases.StudentProgressUseCase
{
    public class GetAllProgressByStudentIdUseCase
    {
        private readonly IStudentProgressRepository _studentProgressRepository;

        public GetAllProgressByStudentIdUseCase(IStudentProgressRepository studentProgressRepository)
        {
            _studentProgressRepository = studentProgressRepository;
        }

        public async Task<List<StudentProgressDTO>> ExecuteAsync(string studentId)
        {
            return await _studentProgressRepository.GetStudentProgressById(studentId);
        }
    }
}
