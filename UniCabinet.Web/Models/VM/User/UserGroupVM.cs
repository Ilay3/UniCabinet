using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniCabinet.Web.Models.VM.User
{
    public class UserGroupVM
    {
        public string UserId { get; set; }
        public int? GroupId { get; set; }
        public List<SelectListItem> AvailableGroups { get; set; }
        public string FullName { get; set; }
    }


}
