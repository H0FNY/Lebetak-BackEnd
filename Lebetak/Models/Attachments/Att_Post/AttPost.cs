using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AttPost : BaseAttachment
    {
        // Navigation property
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
