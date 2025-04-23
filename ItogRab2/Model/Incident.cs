using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItogRab2.Model
{
    public class Incident
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DataReported { get; set; }
        public string Status { get; set; }
    }
}
