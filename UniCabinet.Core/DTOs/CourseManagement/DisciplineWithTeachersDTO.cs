using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Core.DTOs.CourseManagement;

public class DisciplineWithTeachersDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SpecialtyName { get; set; }
    public string Description { get; set; }
    public List<UserDTO> Teachers { get; set; }
}
