using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Core.DTOs.SpecializationManagement
{
    public class UserSpecialtiesAndDisciplinesDTO
    {
        public List<SpecializationDTO> Specialties { get; set; }
        public List<DisciplineDTO> Disciplines { get; set; }
    }
}
