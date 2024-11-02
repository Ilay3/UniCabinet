namespace UniCabinet.Web.ViewModel.Lecture
{
    public class LectureAttendanceViewModel
    {
        public int LectureId { get; set; }
        public int DisciplineDetailId { get; set; }

        public List<StudentAttendanceViewModel> Students { get; set; }
    }

    

}
