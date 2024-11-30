using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Core.DTOs.UserManagement
{
    public class UserDetailDTO: BaseUserDTO
    {
        public string Id { get; set; }

        public List<string> Roles { get; set; }
    }
}
