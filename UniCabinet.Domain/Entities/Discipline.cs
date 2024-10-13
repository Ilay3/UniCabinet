using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Discipline
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        // Навигационные свойства
        public ICollection<DisciplineDetail> DisciplineDetails { get; set; }
    }
}
