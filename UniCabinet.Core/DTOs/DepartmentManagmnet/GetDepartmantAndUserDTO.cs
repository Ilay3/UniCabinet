using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Core.DTOs.DepartmentManagmnet
{
    public class GetDepartmantAndUserDTO
    {
        public List<UserDTO> User { get; set; }
        public List<DisciplineDTO> Discipline { get; set; }

    }
}
