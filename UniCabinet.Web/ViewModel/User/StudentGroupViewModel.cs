using UniCabinet.Domain.DTO;
using UniCabinet.Web.Models;

namespace UniCabinet.Web.ViewModel.User
{
    public class StudentGroupViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
