using UniCabinet.Core.Models.ViewModel.Common;
using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Core.DTOs.SpecializationManagement
{
    public class SpecializationListDTO:SpecializationBaseVM
    {
        public int Id { get; set; }
        public List<UserVM> Teacher { get; set; }
    }
}
