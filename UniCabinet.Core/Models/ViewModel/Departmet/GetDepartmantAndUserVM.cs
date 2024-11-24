using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Core.Models.ViewModel.Departmet
{
    public class GetDepartmantAndUserVM
    {
        public List<UserDTO> User { get; set; }
        public List<DisciplineDTO> Discipline { get; set; }
    }
}
