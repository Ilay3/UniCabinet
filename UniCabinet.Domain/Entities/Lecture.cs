using System;
using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Lecture
    {
        public int LectureId { get; set; }
        public int DisciplineOfferingId { get; set; }
        public DisciplineOffering DisciplineOffering { get; set; }

        public int LectureNumber { get; set; }
        public DateTime Date { get; set; }

        // Навигационные свойства
        public ICollection<LectureAttendance> LectureAttendances { get; set; }
    }
}
