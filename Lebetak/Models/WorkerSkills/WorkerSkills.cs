namespace Lebetak.Models
{
    public class WorkerSkills
    {
        public string WorkerId { get; set; }
        public int  SkillId { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
