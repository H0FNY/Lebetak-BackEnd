using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class AttCompanyProject: BaseAttachment
    {
        // Relation
        public int ProjectId { get; set; }
        public virtual ProjectCompany Project { get; set; }
    }
}
