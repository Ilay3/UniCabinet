namespace UniCabinet.Core.Models.ViewModel.Departmet
{
    public class DepartmentWithDisciplinesVM
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public List<DisciplineWithTeachersVM> Disciplines { get; set; }
    }
}
