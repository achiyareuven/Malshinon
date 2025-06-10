using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Models
{
    public class Rports
    {
        public int Id { get; set; }
        public int porterId { get; set; }
        public int TargetId { get; set; }
        public string Text { get; set; }
        public DateTime TimesTamp {  get; set; }
    }
}
