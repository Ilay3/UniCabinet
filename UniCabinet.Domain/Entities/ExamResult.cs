namespace UniCabinet.Domain.Entities
{
    public class ExamResult
    {
        public int ExamResultId { get; set; }
        public string StudentId { get; set; }
        public User Student { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public decimal CalculatedGrade { get; set; }
        public int FinalGrade { get; set; }
        public bool IsAutomatic { get; set; }
    }
}
