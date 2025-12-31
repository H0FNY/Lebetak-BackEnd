using Lebetak.Models.ChatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Worker
    {
        public string Description { get; set; }
        public int ExperienceYears { get; set; }
        public double Rate { get; set; }=0.0;
        public int NumberOfRates { get; set; }=0;
        public bool Is_Online { get; set; } = false;
        public bool Is_Booked { get; set; } = false;
        public bool Is_Avilable { get; set; } = false;
        public int HourlyPrice { get; set; }

        // Navigation Properties
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<WorkerSkills>? WorkerSkills { get; set; }
        public virtual ICollection<ProjectWorker>? Projects { get; set; }
        public virtual ICollection<Proposal>? Proposals { get; set; }
        public virtual ICollection<Chat>? Chats { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
    }
}