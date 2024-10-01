using System;
using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Exam
    {
        public int Id { get; set; }

        public int DisciplineDetailId { get; set; }
        public DisciplineDetail DisciplineDetails { get; set; }

        public DateTime Date { get; set; }

        // Навигационные свойства
        public ICollection<ExamResult> ExamResults { get; set; }
    }
}
