using Lebetak.Models;

namespace Lebetak.Repository
{
    public class ChatNotificationRepositry : BaseRepo<ChatNotification>
    {
        public ChatNotificationRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
