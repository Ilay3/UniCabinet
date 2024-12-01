namespace UniCabinet.Core.DTOs.CourseManagement;

public class DepartmentWithDisciplinesDTO
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public List<DisciplineWithTeachersDTO> Disciplines { get; set; }
}
