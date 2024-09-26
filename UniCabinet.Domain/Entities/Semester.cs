using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UniCabinet.Domain.Entities
{
    public class Semester
    {
        public int SemesterId { get; set; }
        public int SemesterNumber { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Навигационные свойства
        public ICollection<DisciplineOffering> DisciplineOfferings { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
