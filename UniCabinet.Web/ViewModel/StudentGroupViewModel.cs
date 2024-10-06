using UniCabinet.Domain.DTO;

namespace UniCabinet.Web.ViewModel
{
    public class StudentGroupViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
    }

}
