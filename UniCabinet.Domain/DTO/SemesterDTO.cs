using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Domain.DTO
{
    public class SemesterDTO
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
    }
}
