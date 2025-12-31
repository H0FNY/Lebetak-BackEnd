using Lebetak.Models;

namespace Lebetak.Repository
{
    public class SkillRepositry : BaseRepo<Skill>
    {
        private LebetakContext context;
        public SkillRepositry(LebetakContext _context) : base(_context)
        {
            context = _context;
        }

        public IQueryable<Skill> FirstOrDefualt(string name)
        {
            return context.Skills.Where(w => w.Name==name);
        }
    }
}
