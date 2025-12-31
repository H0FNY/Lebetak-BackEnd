using Lebetak.Common.Enumes;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Location { get; set; }
        public decimal BudgetFrom { get; set; }
        public decimal BudgetTo { get; set; }
        

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreatedDate { get; set; }

        // job status
        public JobStatus Status { get; set; } = JobStatus.Open;


        // Navigation property
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int UrgencyId { get; set; }
        public virtual Urgency Urgency { get; set; }
        //public int PostStatusId { get; set; }
        //public virtual PostStatus PostStatus { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Proposal>? Proposals { get; set; }
        public virtual ICollection<AttPost>? AttPosts { get; set; }
    }
}
