namespace UniCabinet.Core.DTOs.TeacherManagment
{
    public class GroupStudentsProgressPageDTO
    {
        public string TypeGroup { get; set; }
        public int CourseNumber { get; set; }
        public int SemesterNumber { get; set; }

        public decimal AvgLecturePoints { get; set; }
        public decimal AvgPracticalPoints { get; set; }
        public decimal AvgTotalPoints { get; set; }

        public List<StudentGroupProgressDTO> StudentsProgress { get; set; }
    }
}
