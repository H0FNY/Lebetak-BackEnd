using Lebetak.Models;

namespace Lebetak.Repository
{
    public class ProjectCompanyRepositry : BaseRepo<ProjectCompany>
    {
        public ProjectCompanyRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
