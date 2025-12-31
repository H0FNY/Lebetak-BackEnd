using Lebetak.Models;

namespace Lebetak.Repository
{
    public class ProjectWorkerRepositry : BaseRepo<ProjectWorker>
    {
        public ProjectWorkerRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
