using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniCabinet.Web.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string DateBirthday { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public IEnumerable<SelectListItem> Groups { get; set; }
        public List<string> SelectedRoles { get; set; }
    }

}
