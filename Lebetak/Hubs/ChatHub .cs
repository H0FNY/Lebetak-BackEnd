using Lebetak.Models;
using Lebetak.Models.ChatModel;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
[Authorize]
public class ChatHub : Hub
{
    private readonly LebetakContext _context;
    public ChatHub(LebetakContext context)
    {
        _context = context;
    }
    public async Task JoinChat(int chatId)
    {
        var userId = Context.UserIdentifier;
        var chat = await _context.Chats.FindAsync(chatId);
        if (chat != null && (chat.clientId == userId || chat.WorkerId == userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat_{chatId}");
        }
    }
    public async Task SendMessage(int chatId, string content)
    {
        var userId = Context.UserIdentifier;
        var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);

        if (chat == null) return;
        if (chat.clientId != userId && chat.WorkerId != userId) return; // Unauthorized
        var message = new Message
        {
            chatId = chatId,
            Content = content,
            SentAt = DateTime.UtcNow,
            IsFromClient = chat.clientId == userId
        };
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        

        var notification = new ChatNotification
        {
            UserId = message.IsFromClient ? chat.WorkerId : chat.clientId,
            ChatId = chatId,
            Message = "تم ارسال رسالة جديدة اليك"
        };

        _context.ChatNotifications.Add(notification);
        await _context.SaveChangesAsync();
        // Broadcast only to the participants in this specific one-to-one group
        await Clients.Group($"chat_{chatId}").SendAsync("ReceiveMessage", new
        {
            id = message.Id,
            chatId = message.chatId,
            content = message.Content,
            sentAt = message.SentAt,
            isFromClient = message.IsFromClient
        });

    }
    public async Task<int> CreateOrGetChat(string workerId)
    {
        var clientId = Context.UserIdentifier;

        // Find existing one-to-one chat
        var chat = await _context.Chats
            .FirstOrDefaultAsync(c => c.clientId == clientId && c.WorkerId == workerId);
        if (chat == null)
        {
            chat = new Chat { clientId = clientId, WorkerId = workerId };
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }
        return chat.Id;
    }
}

//using Lebetak.Models;
//using Lebetak.Models.ChatModel;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//[Authorize]
//public class ChatHub : Hub
//{

//    private readonly LebetakContext _context;

//    public static Dictionary<string, string> OnlineUsers = new();

//    public ChatHub(LebetakContext context)
//    {
//        _context = context;
//    }

//    public override async Task OnConnectedAsync()
//    {
//        var userId = Context.UserIdentifier;
//        if (!string.IsNullOrEmpty(userId) && !OnlineUsers.ContainsKey(userId))
//        {
//            OnlineUsers.Add(userId, Context.ConnectionId);
//        }
//        await base.OnConnectedAsync();
//    }

//    public override async Task OnDisconnectedAsync(Exception? exception)
//    {
//        var userId = Context.UserIdentifier;
//        if (!string.IsNullOrEmpty(userId) && OnlineUsers.ContainsKey(userId))
//        {
//            OnlineUsers.Remove(userId);
//        }
//        await base.OnDisconnectedAsync(exception);
//    }

//    public async Task JoinChat(int chatId)
//    {
//        await Groups.AddToGroupAsync(Context.ConnectionId, $"Chat-{chatId}");
//    }

//    public async Task SendMessage(int chatId, string content)
//    {
//        var userId = Context.UserIdentifier;
//        if (string.IsNullOrEmpty(userId)) return;

//        var chat = await _context.Chats
//            .Include(c => c.client)
//            .Include(c => c.Worker)
//            .FirstOrDefaultAsync(c => c.Id == chatId);

//        if (chat == null)
//        {
//            var worker = await _context.Workers.FirstOrDefaultAsync();
//            var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == userId);

//            if (client == null || worker == null) return;

//            chat = new Chat
//            {
//                clientId = client.UserId,
//                WorkerId = worker.UserId,
//                Messages = new List<Message>()
//            };

//            _context.Chats.Add(chat);
//            await _context.SaveChangesAsync();
//        }

//        bool isFromClient = chat.client.UserId == userId;

//        var message = new Message
//        {
//            chatId = chat.Id,
//            Content = content,
//            SentAt = DateTime.UtcNow,
//            IsFromClient = isFromClient
//        };

//        _context.Messages.Add(message);
//        await _context.SaveChangesAsync();

//        await Clients.Group($"Chat-{chat.Id}").SendAsync("ReceiveMessage", message);


//        string receiverId = isFromClient ? chat.Worker.UserId : chat.client.UserId;
//        if (OnlineUsers.ContainsKey(receiverId))
//        {
//            string connectionId = OnlineUsers[receiverId];
//            await Clients.Client(connectionId).SendAsync("NewNotification", new
//            {
//                Title = "رسالة جديدة على حسابك",
//                Body = content,
//                ChatId = chat.Id
//            });
//        }
//    }
//}

#region test

// for test 

//using Lebetak.Models;
//using Lebetak.Models.ChatModel;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;

//public class ChatHub : Hub
//{
//    private readonly LebetakContext _context;

//    public static Dictionary<string, string> OnlineUsers = new();

//    public ChatHub(LebetakContext context)
//    {
//        _context = context;
//    }

//    public override async Task OnConnectedAsync()
//    {
//        var httpContext = Context.GetHttpContext();
//        var userId = httpContext.Request.Query["userId"].ToString();

//        if (!string.IsNullOrEmpty(userId) && !OnlineUsers.ContainsKey(userId))
//        {
//            OnlineUsers.Add(userId, Context.ConnectionId);
//        }
//        await base.OnConnectedAsync();
//    }

//    public override async Task OnDisconnectedAsync(Exception? exception)
//    {
//        var httpContext = Context.GetHttpContext();
//        var userId = httpContext.Request.Query["userId"].ToString();

//        if (!string.IsNullOrEmpty(userId) && OnlineUsers.ContainsKey(userId))
//        {
//            OnlineUsers.Remove(userId);
//        }
//        await base.OnDisconnectedAsync(exception);
//    }

//    public async Task JoinChat(int chatId)
//    {
//        await Groups.AddToGroupAsync(Context.ConnectionId, $"Chat-{chatId}");
//    }

//    public async Task SendMessage(int chatId, string content)
//    {
//        var httpContext = Context.GetHttpContext();
//        var userId = httpContext.Request.Query["userId"].ToString();
//        if (string.IsNullOrEmpty(userId)) return;

//        var chat = await _context.Chats
//            .Include(c => c.client)
//            .Include(c => c.Worker)
//            .FirstOrDefaultAsync(c => c.Id == chatId);

//        if (chat == null)
//        {
//            var worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == "b82556b7-f110-44a5-a40e-d4762d138cc0");
//            var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == userId);

//            if (client == null || worker == null) return;

//            chat = new Chat
//            {
//                clientId = client.UserId,
//                WorkerId = worker.UserId,
//                Messages = new List<Message>()
//            };

//            _context.Chats.Add(chat);
//            await _context.SaveChangesAsync();
//        }

//        bool isFromClient = chat.client.UserId == userId;

//        var message = new Message
//        {
//            chatId = chat.Id,
//            Content = content,
//            SentAt = DateTime.UtcNow,
//            IsFromClient = isFromClient
//        };

//        _context.Messages.Add(message);
//        await _context.SaveChangesAsync();

//        await Clients.Group($"Chat-{chat.Id}").SendAsync("ReceiveMessage", message);

//        string receiverId = isFromClient ? chat.Worker.UserId : chat.client.UserId;
//        if (OnlineUsers.ContainsKey(receiverId))
//        {
//            string connectionId = OnlineUsers[receiverId];
//            await Clients.Client(connectionId).SendAsync("NewNotification", new
//            {
//                Title = "رسالة جديدة على حسابك",
//                Body = content,
//                ChatId = chat.Id
//            });
//        }
//    }
//}

#endregion