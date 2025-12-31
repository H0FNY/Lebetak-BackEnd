using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Owner
    {
        public string UserID { get; set; }
        public virtual User User { get; set; }

        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }
    }
}
