using System.Collections.Generic;
using UniCabinet.Domain.DTO;

namespace UniCabinet.Web.ViewModel.Practical
{
    public class PracticalListViewModel
    {
        public int DisciplineDetailId { get; set; }
        public int CurrentPracticalCount { get; set; }
        public int MaxPracticalCount { get; set; }
        public List<PracticalDTO> Practicals { get; set; }
    }
}
