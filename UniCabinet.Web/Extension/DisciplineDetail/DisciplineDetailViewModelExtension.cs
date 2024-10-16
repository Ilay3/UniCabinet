using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.DisiciplineDetail;

namespace UniCabinet.Web.Extension.DisciplineDetail
{
    public static class DisciplineDetailViewModelExtension
    {
        public static DisciplineDetailViewModel GetDisciplineDetailViewModel(this DisciplineDetailDTO modelDTO)
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
                SemesterNumber = modelDTO.SemesterNumber,
                
                GroupName = modelDTO.GroupName,
                DisciplineName = modelDTO.DisciplineName,
                TeacherFirstName = modelDTO.TeacherFirstName,
                TeacherLastName = modelDTO.TeacherLastName,
                TeacherPatronymic = modelDTO.TeacherPatronymic,
            };

            return disciplineD;
        }
    }
}
