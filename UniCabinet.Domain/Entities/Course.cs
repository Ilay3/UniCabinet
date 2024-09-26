using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UniCabinet.Domain.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string Description { get; set; }

        // Навигационные свойства
        public ICollection<Semester> Semesters { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
