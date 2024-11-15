using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Core.DTOs.UserManagement
{
    public class UserDTO : BaseUserDTO
    {
        public List<string> Roles { get; set; }
        public string GroupName { get; set; }
        public int? GroupId { get; set; }
    }
}

