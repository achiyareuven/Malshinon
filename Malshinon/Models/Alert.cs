using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public  class Alert
    {
        public int ID { get; set; }
        public int TargetId { get; set; }
        public string Reason { get; set; }
        public DateTime? WindowStart { get; set; } = null;
        public DateTime? WindowEnd { get; set; } = null ;
        public DateTime AlertTime { get; set; }
    }
}
