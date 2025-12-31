using System.ComponentModel.DataAnnotations.Schema;

namespace Lebetak.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }


        // Navigation Properties
        //public string WorkerSkillId { get; set; }
        public virtual ICollection<WorkerSkills>? WorkerSkills { get; set; }


    }
}
