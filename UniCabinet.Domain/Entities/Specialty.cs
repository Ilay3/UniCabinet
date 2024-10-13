using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCabinet.Domain.Entities
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Teachers { get; set; }
    }
}
