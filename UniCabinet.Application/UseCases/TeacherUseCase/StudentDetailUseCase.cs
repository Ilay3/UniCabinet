using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.StudentProgressManagment;
namespace UniCabinet.Application.UseCases.TeacherUseCase
{
    public class StudentDetailUseCase
    {
        private readonly IStudentProgressRepository _studentProgressRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        public StudentDetailUseCase(IStudentProgressRepository studentProgressRepository, IDisciplineDetailRepository disciplineDetailRepository)
        {
            _studentProgressRepository = studentProgressRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
        }
        public async Task<List<StudentProgressDTO>> ExecuteAsync(string studentId, int disciplineId, int groupId)
        {
            try
            {

                var disciplineDetail = await _disciplineDetailRepository
                    .GetByGroupAndDisciplineAsync(groupId, disciplineId);
                var progressList = await _studentProgressRepository.GetAllStudentProgressById(studentId);
                var filteredProgress = progressList
                    .Where(sp => sp.DisciplineDetailId == disciplineDetail.Id)
                    .ToList();
                return filteredProgress;
            }
            catch
            {
                throw;
            }
        }
    }
}
