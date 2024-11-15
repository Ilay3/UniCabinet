using UniCabinet.Core.Models.ViewModel.Common;
namespace UniCabinet.Core.Models.ViewModel.User
{
    public class UserGroupVM
    {
        public string UserId { get; set; }
        public int? GroupId { get; set; }
        public List<SelectListItemVM> AvailableGroups { get; set; }
        public string FullName { get; set; }
    }


}
