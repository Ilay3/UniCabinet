using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniCabinet.Web.ViewModel.User
{
    public class UserGroupViewModel
    {
        public string UserId { get; set; }
        public int? GroupId { get; set; }
        public List<SelectListItem> AvailableGroups { get; set; }
        public string FullName { get; set; }
    }


}
