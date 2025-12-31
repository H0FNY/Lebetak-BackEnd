using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string NumberPhone { get; set; }

        // Navigation Properties
        public virtual Company Company { get; set; }
    }
}
