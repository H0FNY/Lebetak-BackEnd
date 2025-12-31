using Lebetak.Models;

namespace Lebetak.Repository
{
    public class NotificationRepository : BaseRepo<Notification>
    {
        public NotificationRepository(LebetakContext _context) : base(_context)
        {
        }
    }
}
