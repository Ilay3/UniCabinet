using System;
using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Exam
    {
        public int ExamId { get; set; }
        public int DisciplineOfferingId { get; set; }
        public DisciplineOffering DisciplineOffering { get; set; }

        public DateTime Date { get; set; }

        // Навигационные свойства
        public ICollection<ExamResult> ExamResults { get; set; }
    }
}
