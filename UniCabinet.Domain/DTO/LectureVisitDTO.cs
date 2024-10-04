using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Domain.DTO
{
    public class LectureVisitDTO
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public int LectureId { get; set; }

        /// <summary>
        /// Посещаемость
        /// </summary>
        public bool isVisit { get; set; }

        /// <summary>
        /// Начисленные баллы
        /// </summary>
        public decimal PointsCount { get; set; }
    }
}
