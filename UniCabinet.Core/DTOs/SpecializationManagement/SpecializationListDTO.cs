using UniCabinet.Core.DTOs.Common;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Core.DTOs.SpecializationManagement
{
    public class SpecializationListDTO : SpecializationBaseDTO
    {
        public int Id { get; set; }
        public List<UserDTO> Teacher { get; set; }
    }
}
