namespace UniCabinet.Domain.Entities
{
    public class StudentProgress
    {
        public int StudentProgressId { get; set; }

        public string StudentId { get; set; }
        public User Student { get; set; }

        public int DisciplineOfferingId { get; set; }
        public DisciplineOffering DisciplineOffering { get; set; }

        public int TotalLecturePoints { get; set; }
        public int TotalPracticalPoints { get; set; }
        public int TotalPoints { get; set; }
        public int FinalGrade { get; set; }
        public bool NeedsRetake { get; set; }
    }
}
