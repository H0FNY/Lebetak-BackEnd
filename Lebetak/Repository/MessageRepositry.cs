using Lebetak.Models;
using Lebetak.Models.ChatModel;

namespace Lebetak.Repository
{
    public class MessageRepositry : BaseRepo<Message>
    {
        public MessageRepositry(LebetakContext _context) : base(_context)
        {
        }
    }
}
