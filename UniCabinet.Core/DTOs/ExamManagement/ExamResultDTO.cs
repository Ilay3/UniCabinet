namespace UniCabinet.Core.DTOs.ExamManagement
{
    public class ExamResultDTO
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int ExamId { get; set; }

        public decimal PointAvarage { get; set; }
        public decimal FinalPoint { get; set; }
        public bool IsAutomatic { get; set; }

        
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentPatronymic { get; set; }
    }

}
