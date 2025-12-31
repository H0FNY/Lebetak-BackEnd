using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Att_Proposal : BaseAttachment
    {
        // Relation 
        public int ProposalId { get; set; }
        public virtual Proposal Proposal { get; set; }

    }
}
