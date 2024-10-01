using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UniCabinet.Domain.Entities
{
    public class Semester
    {
        public int Id { get; set; }

        /// <summary>
        /// Номер семестра
        /// </summary>
        public int Number { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        /// <summary>
        /// Начало семестра
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Конец семестра
        /// </summary>
        public DateTime EndDate { get; set; }

        // Навигационные свойства
        public ICollection<DisciplineDetail> DisciplineDetials { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
