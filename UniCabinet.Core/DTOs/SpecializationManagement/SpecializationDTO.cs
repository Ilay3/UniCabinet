
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Core.DTOs.SpecializationManagement
{
    public class SpecializationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserDTO> Teacher { get; set; }


    }
}
