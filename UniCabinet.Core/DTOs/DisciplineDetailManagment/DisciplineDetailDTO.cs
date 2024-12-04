namespace UniCabinet.Core.DTOs.DisciplineDetailManagment
{
    public class DisciplineDetailDTO
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int CourseName { get; set; }

        public int DisciplineId { get; set; }

        public string DisciplineName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int SemesterId { get; set; }

        public int SemesterName { get; set; }

        public string TeacherId { get; set; }

        public string TeacherName { get; set; }


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
