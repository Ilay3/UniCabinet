using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Domain.DTO
{
    public class ExamDTO
    {
        public int Id { get; set; }

        public int DisciplineDetailId { get; set; }

        public DateTime Date { get; set; }
    }
}
