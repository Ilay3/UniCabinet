using UniCabinet.Core.Models.ViewModel.Common;

namespace UniCabinet.Core.Models.ViewModel.DisciplineDetail
{
    public class FilterOptionsVM
    {
        public List<SelectListItemVM> Courses { get; set; }
        public List<SelectListItemVM> Semesters { get; set; }
        public List<SelectListItemVM> Groups { get; set; }
    }

}
