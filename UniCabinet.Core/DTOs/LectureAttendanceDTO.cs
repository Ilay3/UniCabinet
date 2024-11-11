namespace UniCabinet.Core.DTOs
{
    public class LectureAttendanceDTO
    {
        public int LectureId { get; set; }

        public int LectureNumber { get; set; }
        public int DisciplineDetailId { get; set; }
        public string DisciplineName { get; set; }

        public List<StudentAttendanceDTO> Students { get; set; }
    }
}
