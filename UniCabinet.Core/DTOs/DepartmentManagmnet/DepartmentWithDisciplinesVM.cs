namespace UniCabinet.Core.DTOs.DepartmentManagmnet
{
    public class DepartmentWithDisciplinesVM
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public List<DisciplineWithTeachersVM> Disciplines { get; set; }
    }
}
