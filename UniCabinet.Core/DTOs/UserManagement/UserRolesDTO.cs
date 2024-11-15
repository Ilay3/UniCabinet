using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Core.DTOs.UserManagement
{
    public class UserRolesDTO
    {
        public string UserId { get; set; }
        public List<string> SelectedRoles { get; set; }
        public List<SelectListItemDTO> AvailableRoles { get; set; }
        public string FullName { get; set; }
    }
}
