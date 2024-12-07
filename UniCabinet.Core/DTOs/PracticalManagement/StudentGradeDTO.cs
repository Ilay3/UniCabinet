namespace UniCabinet.Core.DTOs.StudentManagement
{
    public class StudentGradeDTO
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int? Grade { get; set; }
    }
}
