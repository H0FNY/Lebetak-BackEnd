using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AttService:BaseAttachment
    {
        // Relation
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
