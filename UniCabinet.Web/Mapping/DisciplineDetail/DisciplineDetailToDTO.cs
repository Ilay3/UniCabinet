using UniCabinet.Domain.DTO;
using UniCabinet.Web.ViewModel.DisiciplineDetail;


namespace UniCabinet.Web.Mapping.DisciplineDetail
{
    public static class DisciplineDetailToDTO
    {
        public static DisciplineDetailDTO GetDisciplineDetailDTO(this DisciplineDetailAddViewModel viewModel)
        {
            var disciplineD = new DisciplineDetailDTO
            {
                LectureCount = viewModel.LectureCount,
                ExamCount = viewModel.ExamCount,
                MinLecturesRequired = viewModel.MinLecturesRequired,
                MinPracticalsRequired = viewModel.MinPracticalsRequired,
                SubExamCount = viewModel.SubExamCount,
                AutoExamThreshold = viewModel.AutoExamThreshold,
                PassCount = viewModel.PassCount,
                PracticalCount = viewModel.PracticalCount,
                DisciplineId = viewModel.DisciplineId,
                SemesterId = viewModel.SemesterId,
                GroupId = viewModel.GroupId,
                TeacherId = viewModel.TeacherId,
            };

            return disciplineD;
        }
    }
}
