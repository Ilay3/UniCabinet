using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniCabinet.Web.Models.VM.User
{
    public class UserRolesVM

    {
        public string UserId { get; set; }
        public List<string> SelectedRoles { get; set; }
        public List<SelectListItem> AvailableRoles { get; set; }
        public string FullName { get; set; }
    }
}
