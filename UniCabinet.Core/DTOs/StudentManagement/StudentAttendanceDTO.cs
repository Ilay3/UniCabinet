namespace UniCabinet.Core.DTOs.StudentManagement
{
    public class StudentAttendanceDTO
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public bool IsPresent { get; set; }
    }
}
