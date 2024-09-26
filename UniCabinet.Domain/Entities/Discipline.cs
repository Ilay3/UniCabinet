using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Навигационные свойства
        public ICollection<DisciplineOffering> DisciplineOfferings { get; set; }
    }
}
