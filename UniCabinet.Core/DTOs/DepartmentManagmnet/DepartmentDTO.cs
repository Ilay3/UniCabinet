using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Core.DTOs.DepartmentManagmnet;

public class DepartmentDTO
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public string UserId { get; set; }

    public List<DisciplineDTO> Discipline { get; set; }

}
