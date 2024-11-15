using UniCabinet.Core.Models.ViewModel.Common;

namespace UniCabinet.Core.Models.ViewModel.User
{
    public class UserRolesVM

    {
        public string UserId { get; set; }
        public List<string> SelectedRoles { get; set; }
        public List<SelectListItemVM> AvailableRoles { get; set; }
        public string FullName { get; set; }
    }
}
