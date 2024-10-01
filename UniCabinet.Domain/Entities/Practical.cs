using System;
using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Practical
    {
        public int Id { get; set; }

        public int DisciplineDetailId { get; set; }
        public DisciplineDetail DisciplineDetails { get; set; }

        /// <summary>
        /// Номер практической
        /// </summary>
        public int PracticalNumber { get; set; }

        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime Date { get; set; }

        // Навигационные свойства
        public ICollection<PracticalResult> PracticalResults { get; set; }
    }
}
