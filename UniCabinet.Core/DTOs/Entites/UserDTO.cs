// UniCabinet.Application/DTOs/UserDTO.cs
namespace UniCabinet.Core.DTOs.Entites
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateBirthday { get; set; }
        public List<string> Roles { get; set; }
        public string GroupName { get; set; }
        public int? GroupId { get; set; }
    }
}

