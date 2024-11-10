namespace UniCabinet.Core.Models.ViewModel
{
    public class StudentAttendanceVM
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public bool IsPresent { get; set; }
    }
}
