using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.DisiciplineDetail;

namespace UniCabinet.Web.Extension.DisciplineDetail
{
    public static class DisciplineDetailViewModelExtension
    {
        public static DisciplineDetailViewModel GetDisciplineDetailViewModel(this DisciplineDetailDTO modelDTO,
            int semesterNumber, int courseNumber, string groupName, string disciplineName,
            string teacherFirstName, string teacherLastName, string teacherPatronymic)
        {
            var disciplineD = new DisciplineDetailViewModel
            {
                Id = modelDTO.Id,
                AutoExamThreshold = modelDTO.AutoExamThreshold,
                SubExamCount = modelDTO.SubExamCount,
                ExamCount = modelDTO.ExamCount,
                LectureCount = modelDTO.LectureCount,
                MinLecturesRequired = modelDTO.MinLecturesRequired,
                MinPracticalsRequired = modelDTO.MinPracticalsRequired,
                PassCount = modelDTO.PassCount,
                PracticalCount = modelDTO.PracticalCount,
                SemesterNumber = semesterNumber,
                CourseNumber = courseNumber,
                GroupName = groupName,
                DisciplineName = disciplineName,
                TeacherFirstName = teacherFirstName,
                TeacherLastName = teacherLastName,
                TeacherPatronymic = teacherPatronymic
            };

            return disciplineD;
        }
    }
}
