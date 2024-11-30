using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Core.Models.ViewModel.Departmet
{
     public class DepartmantVM
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public List<DisciplineListVM> Discipline { get; set; }
    }
}
