namespace UniCabinet.Core.Models.ViewModel.Lecture
{
    public class LectureAttendanceVM
    {
        public int LectureId { get; set; }

        public int LectureNumber { get; set; }
        public int DisciplineDetailId { get; set; }
        public string DisciplineName { get; set; }

        public List<StudentAttendanceVM> Students { get; set; }
    }



}
