using Lebetak.Models;

namespace Lebetak.Repository
{
    public class ProposalNotificationRepositry : BaseRepo<ProposalNotification>
    {
        public ProposalNotificationRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
