using Lebetak.Models.Attachments.Att_Worker_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class ProjectCompany
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalReacts { get; set; }

        // Navigation Properties
        // company
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<AttCompanyProject> Images { get; set; }

    }
}
