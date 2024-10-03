using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCabinet.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        /// <summary>
        /// Очно/Заочно 
        /// </summary>
        public string TypeGroup { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        // Навигационные свойства
        public ICollection<User> Users { get; set; }

        public ICollection<DisciplineDetail> DisciplineDetails { get; set; }
    }
}
