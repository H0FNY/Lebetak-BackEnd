using Lebetak.DTOs;
using Lebetak.Hubs;
using Lebetak.Models;
using Lebetak.Models.ChatModel;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly UnitOFWork _unitOfWork;
        private readonly IHubContext<Newshub> _hub;
        public ChatController(UnitOFWork unitOfWork, IHubContext<Newshub> hub) 
        { 
            _unitOfWork = unitOfWork; 
            _hub = hub;
        }
        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var chat = await _unitOfWork.ChatRepo.GetChatByIdAsync(chatId);

            if (chat == null || (chat.clientId != userId && chat.WorkerId != userId))
                return Forbid();
            var messages = await _unitOfWork.ChatRepo.GetMessagesAsync(chatId);
            var res=messages.Select(m => new MessageViewDTO
            {
                Id = m.Id,
                Content = m.Content,
                SentAt = m.SentAt,
                IsFromClient = m.IsFromClient, 

            });
            return Ok(res);
        }
        [HttpGet("my-chats")]
        public async Task<IActionResult> GetMyChats()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var chats = await _unitOfWork.ChatRepo.GetUserChatsAsync(userId);

            var result = chats.Select(c => new {
                Id = c.Id,
                OtherUserId = c.clientId == userId ? c.WorkerId : c.clientId,
                OtherUserName= c.clientId == userId ? c.Worker.User.F_Name + " " + c.Worker.User.L_Name : c.client.User.F_Name + " " + c.client.User.L_Name,
                OtherUserPicUrl = c.clientId == userId ? c.Worker.User.profileImageUrl: c.client.User.profileImageUrl,
                LastMessage = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.Content,
                LastMessageTime = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.SentAt
            });

            return Ok(result);
        }

        [Authorize]
        [HttpPost("send/{chatId}")]
        public async Task<IActionResult> SendMessage(int chatId, [FromBody] SendMessageDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                return BadRequest("Message content is required");

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var chat = await _unitOfWork.ChatRepo.GetChatByIdAsync(chatId);

            if (chat == null)
                return NotFound("Chat not found");

            // Security check
            if (chat.clientId != userId && chat.WorkerId != userId)
                return Forbid();

            bool isFromClient = chat.clientId == userId;

            var message = new Message
            {
                chatId = chatId,
                Content = dto.Content,
                SentAt = DateTime.UtcNow,
                IsFromClient = isFromClient,
                IsReaded = false
            };

            _unitOfWork.MessageRepo.Add(message);
            _unitOfWork.Save();

            var notification = new ChatNotification
            {
                UserId = isFromClient ? chat.WorkerId : chat.clientId,
                ChatId = chatId,
                Message = "تم ارسال رسالة جديدة اليك"
            };

            _unitOfWork.ChatNotificationRepo.Add(notification);
            _unitOfWork.Save();

            await _hub.Clients.User(isFromClient ? chat.WorkerId : chat.clientId)
                .SendAsync("ReceiveNotification", notification.Message);


            var result = new MessageViewDTO
            {
                Id = message.Id,
                Content = message.Content,
                SentAt = message.SentAt,
                IsFromClient = message.IsFromClient
            };

            return Ok(result);
        }

        [Authorize]
        [HttpPost("open-or-create")]
        public async Task<IActionResult> OpenOrCreateChat([FromBody] OpenChatDTO dto)
        {
            if (string.IsNullOrEmpty(dto.WorkerId))
                return BadRequest("WorkerId is required");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // تأكد أن المستخدم Client
            var client = _unitOfWork.ClientRepo.GetById(userId);
            if (client == null)
                return Forbid("Only clients can start chats");

            var worker = _unitOfWork.WorkerRepo.GetById(dto.WorkerId);
            if (worker == null)
                return Forbid("there isn't worker");


            // هل يوجد شات بالفعل؟
            var chat = await _unitOfWork.ChatRepo
                .Get(c => c.clientId == userId && c.WorkerId == dto.WorkerId)
                .FirstOrDefaultAsync();

            if (chat == null)
            {
                chat = new Chat
                {
                    clientId = userId,
                    WorkerId = dto.WorkerId,
                    Messages = new List<Message>()
                };

                _unitOfWork.ChatRepo.Add(chat);
                _unitOfWork.Save();
            }

            return Ok(new
            {
                chatId = chat.Id
            });
        }

    }
}
