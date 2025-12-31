using Lebetak.Models.Attachments.Att_Worker_Project;

namespace Lebetak.Models
{
    public class ProjectWorker
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalReacts { get; set; }
        
        // Navigation Properties
        // worker
        public string WorkerId { get; set; }
        public virtual Worker Worker { get; set; }

        // AttWorkerProject ---> Worker Images
        public virtual ICollection<AttWorkerProject> Images { get; set; }

    }
}
