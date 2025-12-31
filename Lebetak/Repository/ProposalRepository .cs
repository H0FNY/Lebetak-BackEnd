using Lebetak.Models;

namespace Lebetak.Repository
{
    public class ProposalRepository : BaseRepo<Proposal>
    {
        public ProposalRepository(LebetakContext _context) : base(_context)
        {
        }
    }
}
