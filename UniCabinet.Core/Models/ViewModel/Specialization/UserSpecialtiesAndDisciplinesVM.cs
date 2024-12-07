using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Core.Models.ViewModel.Discipline;

namespace UniCabinet.Core.Models.ViewModel.Specialization
{
    public class UserSpecialtiesAndDisciplinesVM
    {
        public List<SpecializationDTO> Specialties { get; set; }
        public List<DisciplineWithSpecialtyVM> Disciplines { get; set; }
   }
}
