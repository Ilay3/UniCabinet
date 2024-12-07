using UniCabinet.Core.Models.ViewModel.Common;

namespace UniCabinet.Core.Models.ViewModel.DisciplineDetail
{
    public class DisciplineDetailEditVM
    {
        public int Id { get; set; }
        public int DisciplineId { get; set; }

        public int CourseId { get; set; }
        public int GroupId { get; set; }
        public int SemesterId { get; set; }

        public int LectureCount { get; set; }
        public int PracticalCount { get; set; }
        public int SubExamCount { get; set; }
        public int ExamCount { get; set; }

        public int MinLecturesRequired { get; set; }
        public int MinPracticalsRequired { get; set; }
        public int AutoExamThreshold { get; set; }
        public int PassCount { get; set; }

        public IEnumerable<SelectListItemVM>? Courses { get; set; }
        public IEnumerable<SelectListItemVM>? Groups { get; set; }
        public IEnumerable<SelectListItemVM>? Semesters { get; set; }
    }
}