using Lebetak.Models;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly UnitOFWork _unitOfWork;
        public ChatController(UnitOFWork unitOfWork) => _unitOfWork = unitOfWork;
        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var chat = await _unitOfWork.ChatRepo.GetChatByIdAsync(chatId);

            if (chat == null || (chat.clientId != userId && chat.WorkerId != userId))
                return Forbid();
            var messages = await _unitOfWork.ChatRepo.GetMessagesAsync(chatId);
            return Ok(messages);
        }
        [HttpGet("my-chats")]
        public async Task<IActionResult> GetMyChats()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var chats = await _unitOfWork.ChatRepo.GetUserChatsAsync(userId);

            var result = chats.Select(c => new {
                Id = c.Id,
                OtherUserId = c.clientId == userId ? c.WorkerId : c.clientId,
                //OtherUserName = "Name...", // Join logic here
                OtherUserName= c.clientId == userId ? c.Worker.User.F_Name + " " + c.Worker.User.L_Name : c.client.User.F_Name + " " + c.client.User.L_Name,
                OtherUserPicUrl = c.clientId == userId ? c.client.User.profileImageUrl: c.client.User.profileImageUrl,
                LastMessage = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.Content,
                LastMessageTime = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.SentAt
            });

            return Ok(result);
        }
    }
}
