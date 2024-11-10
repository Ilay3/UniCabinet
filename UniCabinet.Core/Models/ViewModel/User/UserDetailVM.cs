namespace UniCabinet.Core.Models.ViewModel.User
{
    public class UserDetailVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateBirthday { get; set; }
        public List<string> Roles { get; set; }
        public string GroupName { get; set; }
    }
}
