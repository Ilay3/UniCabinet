
using UniCabinet.Core.Models.ViewModel.Common;
using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Core.Models.ViewModel
{

    public class StudentGroupVM
    {
        public List<UserVM> Users { get; set; }
        public List<SelectListItemVM> Groups { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
