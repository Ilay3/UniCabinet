using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Core.DTOs.UserManagement
{
    public class UserGroupDTO
    {
        public string UserId { get; set; }
        public int? GroupId { get; set; }
        public List<SelectListItemDTO> AvailableGroups { get; set; }
        public string FullName { get; set; }
    }
}
