using Lebetak.Models;

namespace Lebetak.Repository
{
    public class WorkerSkillsRepositry : BaseRepo<WorkerSkills>
    {
        public WorkerSkillsRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
