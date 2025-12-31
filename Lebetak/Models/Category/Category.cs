using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lebetak.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? IconURL { get; set; }

        public int CompanyId { get; set; }
        public int QuestionId { get; set; }

        // Navigation Properties
        public virtual ICollection<Company>? Companies { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }

    }
}
