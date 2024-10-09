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

        /// <summary>
        /// День начала семестра
        /// </summary>
        public int DayStart { get; set; }

        /// <summary>
        /// Месяц начала семестра
        /// </summary>
        public int MounthStart { get; set; }

        /// <summary>
        /// День конца семестра
        /// </summary>
        public int DayEnd { get; set; }

        /// <summary>
        /// Месяц конца семестра
        /// </summary>
        public int MounthEnd { get; set; }

        // Навигационные свойства
        public ICollection<DisciplineDetail> DisciplineDetials { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
