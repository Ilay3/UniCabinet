using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Core.Models.ViewModel.Departmet
{
    public class DisciplineWithTeachersVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpecialtyName { get; set; }
        public string Description { get; set; }
        public List<UserVM> Teachers { get; set; }
    }
}
