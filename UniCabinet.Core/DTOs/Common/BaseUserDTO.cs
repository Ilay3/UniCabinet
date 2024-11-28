namespace UniCabinet.Core.DTOs.Common
{
    public class BaseUserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? DateBirthday { get; set; }
    }
}
