using Lebetak.Models;

namespace Lebetak.Repository
{
    public class AttPostRepositry : BaseRepo<AttPost>
    {
        public AttPostRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
