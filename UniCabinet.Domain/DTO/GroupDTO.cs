using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Domain.DTO
{
    public class GroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Очно/Заочно 
        /// </summary>
        public string TypeGroup { get; set; }

        public int CourseId { get; set; }

        public int SemesterId { get; set; }
    }
}
