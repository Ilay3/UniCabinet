using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Core.Models.ViewModel.Specialization
{
    public class SpecializationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserVM>Teacher { get; set; }
    }
}
