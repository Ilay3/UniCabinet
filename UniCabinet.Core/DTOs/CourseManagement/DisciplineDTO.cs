using UniCabinet.Core.DTOs.Common;

namespace UniCabinet.Core.DTOs.CourseManagement
{
    public class DisciplineDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SpecialtyId { get; set; }

        public string SpecialtyName { get; set; }

        public string Description { get; set; }
    }
}
