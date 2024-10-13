using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UniCabinet.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }

        /// <summary>
        /// Номер курса
        /// </summary>
        public int Number { get; set; }

        // Навигационные свойства
        public ICollection<Group> Groups { get; set; }
        public ICollection<DisciplineDetail> DisciplineDetails { get; set; }
    }
}
