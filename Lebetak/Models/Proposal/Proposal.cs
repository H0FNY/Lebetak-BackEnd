using Lebetak.Common.Enumes;
using Lebetak.Models.Attachments.Att_Worker_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public string Description { get; set; }
        //public bool Is_Accepted { get; set; } = false;
        public ProposalStatus Status { get; set; } = ProposalStatus.Waiting;
        public double Price { get; set; }
        public DateTime Created_At { get; set; } = DateTime.UtcNow;


        // Navigation Properties
        //Worker
        public string WorkerId { get; set; }
        public virtual Worker? Worker { get; set; }

        // Images
        public virtual ICollection<Att_Proposal>? Images { get; set; }

        //Rating
        public int RatingId { get; set; }
        public virtual Rating? Rating { get; set; }

        //Post
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
