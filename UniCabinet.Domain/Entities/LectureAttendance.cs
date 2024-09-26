namespace UniCabinet.Domain.Entities
{
    public class LectureAttendance
    {
        public int AttendanceId { get; set; }
        public string StudentId { get; set; }
        public User Student { get; set; }

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public bool WasPresent { get; set; }
        public int PointsAwarded { get; set; }
    }
}
