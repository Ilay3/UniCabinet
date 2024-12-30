namespace UniCabinet.Core.Models.ViewModel.Student
{
    public class StudentProgressVM
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public int DisciplineDetailId { get; set; }
        public string DisciplineName { get; set; }

        public int TotalLecturePoints { get; set; }

        public int TotalPracticalPoints { get; set; }


        public int TotalPoints { get; set; }


        public int FinalGrade { get; set; }

        public bool NeedsRetake { get; set; }
    }
}
