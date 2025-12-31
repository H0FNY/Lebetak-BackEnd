using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime PreferredTime { get; set; }
        public string ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }

        public virtual ICollection<AttService>? AttService { get; set; }
        public virtual ICollection<ServiceAnswer>? ServiceAnswers { get; set; }

    }
}
