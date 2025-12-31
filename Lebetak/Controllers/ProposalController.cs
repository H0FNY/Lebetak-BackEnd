using AutoMapper;
using Lebetak.Common.Enumes;
using Lebetak.DTOs;
using Lebetak.DTOs.Proposal;
using Lebetak.Hubs;
using Lebetak.Models;
using Lebetak.Models.Attachments.Att_Worker_Project;
using Lebetak.Services;
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
    public class ProposalController : ControllerBase
    {
        private readonly UnitOFWork _unitOFWork;
        private readonly IHubContext<Newshub> _hub;
        IMapper _mapper;
        public ProposalController(UnitOFWork unit , IHubContext<Newshub> hub,IMapper mapper)
        {
            _unitOFWork = unit;
            _hub = hub;
            _mapper = mapper;
        }

        


        [HttpGet("getMyProposal")]
        [Authorize]
        public IActionResult GetMyProposal()
        {
            var workerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var worker = _unitOFWork.WorkerRepo
                .Get(w => w.UserId == workerUserId)
                .FirstOrDefault();
            if (worker == null)
                return BadRequest("Worker not found");
            var proposals = _unitOFWork.ProposalRepo
                .Get(p => p.WorkerId == worker.UserId);
            return Ok(_mapper.Map<List<ProposalViewDTO>>(proposals));
        }




        [Authorize]
        [HttpGet("getProposalsForPost/{postId}")]
        public IActionResult GetProposalsForPost(int PostId) {
            var proposals = _unitOFWork.ProposalRepo.Get(p => p.PostId == PostId);
            return Ok(_mapper.Map<List<ProposalViewDTO>>(proposals));
        }


        // Send Proposal
        [Authorize]
        [HttpPost("sendProposal")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SendProposal([FromForm] ProposalAddDTO dto)
        {

            var workerUserId= User.FindFirstValue(ClaimTypes.NameIdentifier);

            var worker = _unitOFWork.WorkerRepo.Get(w => w.UserId == workerUserId).FirstOrDefault();
            if (worker == null)
                return BadRequest("Worker not found");

            var post = _unitOFWork.PostRepo
                .Get(p => p.Id == dto.PostId)
                .FirstOrDefault();

            if (post == null)
                return NotFound("Post not found");
            var isProposalExist = _unitOFWork.ProposalRepo
                .Get(p => p.PostId == dto.PostId && p.WorkerId == worker.UserId)
                .FirstOrDefault();
            if (isProposalExist!=null)
                return BadRequest("You have already sent a proposal for this post");


            var proposal = _mapper.Map<Proposal>(dto);
            proposal.WorkerId = workerUserId;
            if (dto.Files != null)
            {
                List<Att_Proposal> Attprop = new List<Att_Proposal>();
                foreach (var file in dto.Files)
                {
                    var url = await FileUpload.UploadAsync(file, _unitOFWork.Cloudinary, "other");
                    Attprop.Add(new Att_Proposal
                    {
                        URL = url.Url.ToString(),
                    });
                }
                if (proposal.Images == null)
                {
                    proposal.Images = Attprop;
                }
            }

            _unitOFWork.ProposalRepo.Add(proposal);
            _unitOFWork.Save();

            var notification = new Notification
            {
                UserId = post.ClientId,
                Message = "تم إرسال عرض جديد على طلبك"
            };

            _unitOFWork.NotificationRepo.Add(notification);
            _unitOFWork.Save();

            await _hub.Clients.User(post.ClientId)
                .SendAsync("ReceiveNotification", notification.Message);

            return Ok("Proposal sent successfully");
        }

        // Accept Proposal
        [Authorize]
        [HttpPost("accept/{proposalId}")]
        public async Task<IActionResult> AcceptProposal(int proposalId)
        {
            var clientUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var proposal = _unitOFWork.ProposalRepo.Get(p => p.Id == proposalId).FirstOrDefault();

            if (proposal == null)
                return NotFound("Proposal not found");

            if (proposal.Post.ClientId != clientUserId)
                return Forbid("You are not allowed to accept this proposal");

            var allProposals = _unitOFWork.ProposalRepo
                .Get(p => p.PostId == proposal.PostId)
                .ToList();

            foreach (var p in allProposals)
                p.Status = ProposalStatus.NotChoosed;

            proposal.Status = ProposalStatus.Choosed;

            proposal.Post.Status = JobStatus.InProgress;

            _unitOFWork.Save();

            var notification = new Notification
            {
                UserId = proposal.Worker.UserId,
                Message = "تم قبول عرضك أجهز للعمل "
            };

            _unitOFWork.NotificationRepo.Add(notification);
            _unitOFWork.Save();

            await _hub.Clients.User(proposal.Worker.UserId)
                .SendAsync("ReceiveNotification", notification.Message);

            return Ok("Proposal accepted successfully");
        }

    }
}
