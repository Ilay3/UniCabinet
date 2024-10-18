using System;
using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Lecture
    {
        public int Id { get; set; }

        public int DisciplineDetailId { get; set; }
        public DisciplineDetail DisciplineDetails { get; set; }

        /// <summary>
        /// Номер лекции
        /// </summary>
        public int Number { get; set; }

        public DateTime Date { get; set; }

        // Навигационные свойства
        public ICollection<LectureVisit> LectureVisits { get; set; }
    }
}
