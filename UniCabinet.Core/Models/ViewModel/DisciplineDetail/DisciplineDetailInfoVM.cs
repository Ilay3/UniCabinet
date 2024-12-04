namespace UniCabinet.Core.Models.ViewModel.DisciplineDetail
{
    public class DisciplineDetailInfoVM
    {
        public int Id { get; set; }
        public string DisciplineName { get; set; }
        public string CourseName { get; set; }
        public string GroupName { get; set; }
        public string SemesterName { get; set; }
        public int LectureCount { get; set; }
        public int PracticalCount { get; set; }
        public int SubExamCount { get; set; }
        public int ExamCount { get; set; }
        public int MinLecturesRequired { get; set; }
        public int MinPracticalsRequired { get; set; }
        public int AutoExamThreshold { get; set; }
        public int PassCount { get; set; }
    }
}
