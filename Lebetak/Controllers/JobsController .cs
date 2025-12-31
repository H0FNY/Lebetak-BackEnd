//using AutoMapper;
//using Lebetak.Common.Enumes;
//using Lebetak.DTOs.Proposal;
//using Lebetak.Models;
//using Lebetak.UnitOfWork;
//using Microsoft.AspNetCore.Mvc;

//namespace Lebetak.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class JobsController : ControllerBase
//    {
//        private readonly UnitOFWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public JobsController(UnitOFWork unitOfWork , IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        // GET: api/jobs/job-details/5
//        [HttpGet("job/details/{postId}")]
//        public IActionResult GetJobDetails(int postId)
//        {
//            var post = _unitOfWork.PostRepo.Get(p => p.Id == postId);
//            var proposals = _unitOfWork.ProposalRepo.Get(p => p.PostId == postId);
//            post.First().Proposals = proposals.ToList();



//            if (post == null || !post.Any())
//                return NotFound();

//            return Ok(post.First());
//        }

//        // POST: api/jobs/accept-proposal/5
//        [HttpPost("accept-proposal/{proposalId}")]
//        public IActionResult AcceptProposal(int proposalId)
//        {
//            var proposal = _unitOfWork.ProposalRepo.GetById(proposalId);

//            if (proposal == null)
//                return NotFound();

//            var allProposals = _unitOfWork.ProposalRepo
//                .Get(p => p.PostId == proposal.PostId);

//            foreach (var p in allProposals)
//                p.Is_Accepted = false;

//            proposal.Is_Accepted = true;

//            var post = _unitOfWork.PostRepo.GetById(proposal.PostId);
//            post.Status = JobStatus.InProgress;

//            _unitOfWork.Save();

//            return Ok("Proposal Accepted & Job Started");
//        }

//        // POST: api/jobs/send-proposal
//        [HttpPost("send-proposal")]
//        public IActionResult SendProposal([FromBody] PropasalDto proposal)
//        {
//            if (proposal == null)
//                return BadRequest("Proposal is null");

//            Proposal proposal1 = _mapper.Map<Proposal>(proposal);

//            _unitOfWork.ProposalRepo.Add(proposal1);

//            var post = _unitOfWork.PostRepo.GetById(proposal.PostId);
//            if (post == null)
//                return NotFound("Post not found");

//            var notify = new Notification
//            {
//                UserId = post.ClientId,
//                Message = "تم إرسال عرض جديد على طلبك"
//            };

//            _unitOfWork.NotificationRepo.Add(notify);
//            _unitOfWork.Save();

//            Console.WriteLine("Email sent to client");

//            return Ok("Proposal sent and notification created");
//        }
//    }
//}
