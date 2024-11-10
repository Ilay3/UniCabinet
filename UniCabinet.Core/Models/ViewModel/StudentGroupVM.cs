using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Core.Models.ViewModel
{
    public class StudentGroupVM
    {
        public List<UserVM> Users { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
