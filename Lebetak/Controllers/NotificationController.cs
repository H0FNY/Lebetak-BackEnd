using AutoMapper;
using Lebetak.DTOs;
using Lebetak.Services;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        UnitOFWork _unitOFWork;
        private readonly IMapper imapper;

        public NotificationController(UnitOFWork unitOFWork, IMapper _imappe)
        {
            _unitOFWork = unitOFWork;
            imapper = _imappe;
        }

        [Authorize]
        [HttpGet("notifications")]
        public IActionResult GetNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            var chatNotifications = _unitOFWork.ChatNotificationRepo
                .Get(n => n.UserId == userId)
                .Select(n => new NotificationViewDTO
                {
                    Id = n.Id,
                    Type = "Chat",
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt,
                    ChatId = n.ChatId,
                    PostId = null
                });

            var proposalNotifications = _unitOFWork.ProposalNotificationRepo
                .Get(n => n.UserId == userId)
                .Select(n => new NotificationViewDTO
                {
                    Id = n.Id,
                    Type = "Proposal",
                    Message = n.Message,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt,
                    PostId = n.PostId,
                    ChatId = null
                });

            var notifications = chatNotifications
                .Concat(proposalNotifications)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
            if (notifications == null)
            {
                return NotFound("No notifications found.");
            }
            return Ok(notifications);
        }

        [Authorize]
        [HttpPost("mark-as-read/{notificationId}")]
        public IActionResult MarkAsRead(int notificationId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            var chatNotification = _unitOFWork.ChatNotificationRepo.GetAll().Where(n => n.Id == notificationId && n.UserId == userId).FirstOrDefault();
            if (chatNotification != null)
            {
                chatNotification.IsRead = true;
                _unitOFWork.ChatNotificationRepo.Update(chatNotification);
                _unitOFWork.Save();
                return Ok("Notification marked as read.");
            }
            var proposalNotification = _unitOFWork.ProposalNotificationRepo.GetAll().Where(n => n.Id == notificationId && n.UserId == userId).FirstOrDefault();
            if (proposalNotification != null)
            {
                proposalNotification.IsRead = true;
                _unitOFWork.ProposalNotificationRepo.Update(proposalNotification);
                _unitOFWork.Save();
                return Ok("Notification marked as read.");
            }
            return NotFound("Notification not found.");
        }
    }
}
