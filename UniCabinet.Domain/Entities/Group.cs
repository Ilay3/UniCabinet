using System.Collections.Generic;

namespace UniCabinet.Domain.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int StartYear { get; set; }
        public int CurrentCourseId { get; set; }
        public Course CurrentCourse { get; set; }
        public int CurrentSemesterId { get; set; }
        public Semester CurrentSemester { get; set; }

        // Навигационные свойства
        public ICollection<User> Users { get; set; }
        public ICollection<DisciplineOffering> DisciplineOfferings { get; set; }
    }
}
