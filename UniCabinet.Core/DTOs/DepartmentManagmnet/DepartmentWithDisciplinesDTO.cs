using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Core.DTOs.DepartmentManagmnet;

public class DepartmentWithDisciplinesDTO
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public List<DisciplineWithTeachersDTO> Disciplines { get; set; }
}
