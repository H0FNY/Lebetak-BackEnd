using Lebetak.Models;

namespace Lebetak.Repository
{
    public class UrgencyRepositry : BaseRepo<Urgency>
    {
        public UrgencyRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
