using UniCabinet.Core.DTOs.PracticalManagement;

namespace UniCabinet.Core.Models.ViewModel.Practical
{
    public class PracticalListVM
    {
        public int DisciplineDetailId { get; set; }
        public int CurrentPracticalCount { get; set; }
        public int MaxPracticalCount { get; set; }
        public List<PracticalDTO> Practicals { get; set; }
    }
}
