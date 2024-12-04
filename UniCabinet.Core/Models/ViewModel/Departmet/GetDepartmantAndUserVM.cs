using UniCabinet.Core.Models.ViewModel.Discipline;
using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Core.Models.ViewModel.Departmet
{
    public class GetDepartmantAndUserVM
    {
        public List<UserVM> User { get; set; }
        public List<DisciplineListVM> Discipline { get; set; }
    }
}
