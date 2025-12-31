using Lebetak.Models.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AttAnswer:BaseAttachment
    {
        public int? AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

    }
}
