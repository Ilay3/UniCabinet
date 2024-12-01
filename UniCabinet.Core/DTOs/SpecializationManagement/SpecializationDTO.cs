
using UniCabinet.Core.DTOs.Common;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Core.DTOs.SpecializationManagement
{
    public class SpecializationDTO : SpecializationBaseDTO
    {
        public int Id { get; set; }
        public List<UserDTO> Teacher { get; set; }


    }
}
