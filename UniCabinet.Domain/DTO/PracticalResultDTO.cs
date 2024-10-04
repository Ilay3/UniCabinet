using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Domain.DTO
{
    public class PracticalResultDTO
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public int PracticalId { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Баллы
        /// </summary>
        public int Point { get; set; }
    }
}
