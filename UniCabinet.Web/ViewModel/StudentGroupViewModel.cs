using UniCabinet.Domain.DTO;
using UniCabinet.Web.Models;

namespace UniCabinet.Web.ViewModel
{
    public class StudentGroupViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
        public PaginationModel Pagination { get; set; } 
    }
}
