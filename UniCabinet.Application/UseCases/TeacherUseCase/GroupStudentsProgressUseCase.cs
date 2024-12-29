using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.TeacherManagment;
namespace UniCabinet.Application.UseCases.TeacherUseCase
{
    public class GroupStudentsProgressUseCase
    {
        private readonly IStudentProgressRepository _studentProgressRepository;
        private readonly IUserRepository _userRepository;
        public GroupStudentsProgressUseCase(IStudentProgressRepository studentProgressRepository, IUserRepository userRepository)
        {
            _studentProgressRepository = studentProgressRepository;
            _userRepository = userRepository;
        }
        public async Task<List<StudentGroupProgressDTO>> ExecuteAsync(int groupId, int disciplineId)
        {
            try
            {

                var students = await _userRepository.GetStudentsByGroupIdAsync(groupId);
                var result = new List<StudentGroupProgressDTO>();
                foreach (var student in students)
                {
                    var spList = await _studentProgressRepository.GetAllStudentProgressById(student.Id);
                    var totalProgress = spList.Sum(sp => sp.TotalPoints);
                    result.Add(new StudentGroupProgressDTO
                    {
                        StudentId = student.Id,
                        FullName = $"{student.LastName} {student.FirstName}",
                        TotalPoints = totalProgress,
                    });
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
