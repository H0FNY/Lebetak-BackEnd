using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public string Text { get; set; }

        // Relation
        public int ProposalId { get; set; }
        public virtual Proposal? Proposal { get; set; }
    }
}
