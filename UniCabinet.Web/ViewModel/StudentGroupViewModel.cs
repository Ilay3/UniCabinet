using UniCabinet.Domain.DTO;

namespace UniCabinet.Web.ViewModel
{
    public class StudentGroupViewModel
    {
        public IEnumerable<UserDTO> Students { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
    }

}
