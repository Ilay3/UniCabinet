using System;
using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Practical
    {
        public int PracticalId { get; set; }
        public int DisciplineOfferingId { get; set; }
        public DisciplineOffering DisciplineOffering { get; set; }

        public int PracticalNumber { get; set; }
        public DateTime Date { get; set; }

        // Навигационные свойства
        public ICollection<PracticalResult> PracticalResults { get; set; }
    }
}
