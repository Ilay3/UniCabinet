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
        /// Начало обучениея
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}")]
        public DateTime StartYear { get; set; }

        public int CurrentCourseId { get; set; }
        public Course CurrentCourse { get; set; }

        public int CurrentSemesterId { get; set; }
        public Semester CurrentSemester { get; set; }

        // Навигационные свойства
        public ICollection<User> Users { get; set; }

        public ICollection<DisciplineDetail> DisciplineDetails { get; set; }
    }
}
