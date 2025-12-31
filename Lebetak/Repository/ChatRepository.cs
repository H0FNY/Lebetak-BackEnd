//using Lebetak.Models;
//using Lebetak.Models.ChatModel;

//namespace Lebetak.Repository
//{
//    public class ChatRepository : BaseRepo<Chat>
//    {
//        public ChatRepository(LebetakContext _context) : base(_context)
//        {
//        }
//    }
//}

using Lebetak.Models;
using Lebetak.Models.ChatModel;
using Lebetak.Repository;
using Microsoft.EntityFrameworkCore;
public class ChatRepository : BaseRepo<Chat>
{
    private LebetakContext _context;
    public ChatRepository(LebetakContext _context) : base(_context)
    {
        this._context = _context;
    }

    public async Task<Chat> GetChatByIdAsync(int chatId)
    {
        return await _context.Chats
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == chatId);
    }
    public async Task<Chat> GetChatByParticipantsAsync(string clientId, string workerId)
    {
        return await _context.Chats
            .FirstOrDefaultAsync(c => c.clientId == clientId && c.WorkerId == workerId);
    }
    public async Task<IEnumerable<Message>> GetMessagesAsync(int chatId)
    {
        return await _context.Messages
            .Where(m => m.chatId == chatId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }
    public async Task<IEnumerable<Chat>> GetUserChatsAsync(string userId)
    {
        return await _context.Chats
            .Include(c => c.Messages)
            .Where(c => c.clientId == userId || c.WorkerId == userId)
            .ToListAsync();
    }
    public void AddChat(Chat chat)
    {
        _context.Chats.Add(chat);
    }
    public void AddMessage(Message message)
    {
        _context.Messages.Add(message);
    }
}