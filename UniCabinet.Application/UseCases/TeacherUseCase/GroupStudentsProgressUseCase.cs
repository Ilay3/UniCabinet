using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.TeacherManagment;
namespace UniCabinet.Application.UseCases.TeacherUseCase
{
    public class GroupStudentsProgressUseCase
    {
        private readonly IStudentProgressRepository _studentProgressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;  // Новый репозиторий для групп

        public GroupStudentsProgressUseCase(
            IStudentProgressRepository studentProgressRepository,
            IUserRepository userRepository,
            IGroupRepository groupRepository)
        {
            _studentProgressRepository = studentProgressRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task<GroupStudentsProgressPageDTO> ExecuteAsync(int groupId, int disciplineId)
        {
            // 1. Получаем студентов по группе
            var students = await _userRepository.GetStudentsByGroupIdAsync(groupId);

            // 2. Загружаем данные о группе
            var groupInfo = await _groupRepository.GetGroupByIdAsync(groupId);
            // Предположим, этот метод возвращает модель, содержащую:
            //  - TypeGroup
            //  - Course.Number
            //  - Semester.Number (или аналогичное поле)

            // 3. Считаем прогресс и формируем список
            var result = new List<StudentGroupProgressDTO>();
            var allLecturePoints = 0;
            var allPracticalPoints = 0;
            var allTotalPoints = 0;

            var studentCount = students.Count;

            foreach (var student in students)
            {
                var spList = await _studentProgressRepository.GetAllStudentProgressById(student.Id);

                 spList = spList.Where(sp => sp.DisciplineDetailId == disciplineId).ToList();

                var totalLecturePoints = spList.Sum(sp => sp.TotalLecturePoints);
                var totalPracticalPoints = spList.Sum(sp => sp.TotalPracticalPoints);
                var totalPoints = spList.Sum(sp => sp.TotalPoints);

                allLecturePoints += totalLecturePoints;
                allPracticalPoints += totalPracticalPoints;
                allTotalPoints += totalPoints;

                result.Add(new StudentGroupProgressDTO
                {
                    StudentId = student.Id,
                    FullName = $"{student.LastName} {student.FirstName}",
                    TotalPoints = totalPoints
                });
            }

            decimal avgLecture = 0;
            decimal avgPractical = 0;
            decimal avgTotal = 0;

            if (studentCount > 0)
            {
                avgLecture = (decimal)allLecturePoints / studentCount;
                avgPractical = (decimal)allPracticalPoints / studentCount;
                avgTotal = (decimal)allTotalPoints / studentCount;
            }

            // Возвращаем DTO с данными
            return new GroupStudentsProgressPageDTO
            {
                TypeGroup = groupInfo.TypeGroup,
                CourseNumber = groupInfo.CourseId,
                SemesterNumber = groupInfo.SemesterId,

                AvgLecturePoints = avgLecture,
                AvgPracticalPoints = avgPractical,
                AvgTotalPoints = avgTotal,

                StudentsProgress = result
            };
        }
    }

}
