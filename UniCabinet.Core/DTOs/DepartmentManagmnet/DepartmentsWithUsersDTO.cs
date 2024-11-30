using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Core.DTOs.DepartmentManagmnet;

public class DepartmentsWithUsersDTO
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public List<UserDTO> Users { get; set; }
    public List<DisciplineDTO> Discipline { get; set; }
}
