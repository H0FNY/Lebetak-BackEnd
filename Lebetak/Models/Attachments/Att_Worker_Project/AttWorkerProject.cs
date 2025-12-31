using Lebetak.Models.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models.Attachments.Att_Worker_Project
{
    public class AttWorkerProject: BaseAttachment
    {
        // Relation
        public int ProjectId { get; set; }
        public virtual ProjectWorker Project { get; set; }

    }
}
