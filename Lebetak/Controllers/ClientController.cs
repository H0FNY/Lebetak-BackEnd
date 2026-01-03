using AutoMapper;
using Lebetak.Common.Enumes;
using Lebetak.DTOs;
using Lebetak.DTOs.Proposal;
using Lebetak.DTOs.Report;
using Lebetak.Models;
using Lebetak.Services;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        
        public UnitOFWork _unitOFWork { get; set; }
        IMapper _mapper;
        public ClientController(UnitOFWork _unitOFWork , IMapper _mapper)
        {
            this._unitOFWork = _unitOFWork;
            this._mapper = _mapper;
        }

        [HttpGet("getAll")]
        public IActionResult Index()
        {
            return Ok(_unitOFWork.ClientRepo.GetAll());
        }

        [HttpGet("getById/{id}")]
        public ActionResult GetById(string id)
        {
            var client = _unitOFWork.ClientRepo.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        [HttpGet("SearchByName/{Name}")]
        public ActionResult GetByName(string name)
        {
            var clients = _unitOFWork.ClientRepo.Get(c => c.User.F_Name.Contains(name) || c.User.L_Name.Contains(name));
            if (clients == null || !clients.Any())
            {
                return NotFound();
            }
            return Ok(clients);
        }

        [Authorize]
        [HttpPost("add-post")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AddPost([FromForm] PostAddDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = _unitOFWork.ClientRepo.GetById(userId);
            if (client == null) { return NotFound("client null"); }

            var post = _mapper.Map<Post>(dto);
            post.ClientId = userId;
            post.CreatedDate = DateTime.Now;
            
            if (dto.Images != null)
            {
                List<AttPost> Attposts = new List<AttPost>();
                foreach (var file in dto.Images)
                {
                    var url = await FileUpload.UploadAsync(file,_unitOFWork.Cloudinary,"other");
                    Attposts.Add( new AttPost
                    {
                        URL = url.Url.ToString(),
                    });


                }
                if (post.AttPosts == null)
                {
                    post.AttPosts = Attposts;
                }
            }
            _unitOFWork.PostRepo.Add(post);
            _unitOFWork.Save();
  
            return Ok(post.Id);
        }

        [Authorize]
        [HttpPost("add-service")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateService([FromForm] ServiceAddDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = _unitOFWork.ClientRepo.GetById(userId);
            var service = new Service
            {
                Description = dto.Description,
                PreferredTime = dto.PreferredTime,
                ClientId = userId,
                CompanyId=dto.CompanyId
            };

            if (dto.Images != null)
            {
                List<AttService> AttService = new List<AttService>();
                foreach (var file in dto.Images)
                {
                    var url = await FileUpload.UploadAsync(file, _unitOFWork.Cloudinary, "other");
                    AttService.Add(new AttService
                    {
                        URL = url.Url.ToString(),
                    });
                }
                if (service.AttService == null)
                {
                    service.AttService = AttService;
                }
            }
            _unitOFWork.ServiceRepo.Add(service);
            _unitOFWork.Save();
            var list  = new List<ServiceAnswer>();
            if (dto.Answers != null)
            {
            
                foreach (var answer in dto.Answers)
                {
                list.Add(new ServiceAnswer
                {
                    ServiceId = service.Id,
                    QuestionId = answer.QuestionId,
                    OptionId = answer.OptionId
                });
                }
            }
            service.ServiceAnswers = list;
            _unitOFWork.Save();
            return Ok(service);
        }

        [Authorize]
        [HttpPost("finishPost/{postId}")]
        public ActionResult FinishPost(int postId)
        {
            var post = _unitOFWork.PostRepo.GetById(postId);
            if (post == null)
            {
                return BadRequest("Post not Exist");
            }
            post.Status=JobStatus.Completed;
            _unitOFWork.Save();

            return Ok("Post was Completed");
        }

        [Authorize]
        [HttpPost("makeReview/{proposalId}")]
        public ActionResult MakeReview(int proposalId, [FromBody] MakeReviewDTO dto)
        {
            var proposal=_unitOFWork.ProposalRepo.GetById(proposalId);
            if (proposal == null)
            {
                return BadRequest("Proposal not Exist");
            }
            if (proposal.Rating != null)
                return BadRequest("This proposal already has a review");

            var rating = new Rating
            {
                Value = dto.Value,
                Text = dto.Text,
                ProposalId = proposalId
            };
            proposal.Rating = rating;
            _unitOFWork.RatingRepo.Add(rating);
            _unitOFWork.Save();
            var avgRate = _unitOFWork.ProposalRepo.GetAll().Where(p => p.WorkerId == proposal.WorkerId && p.Rating != null).Average(p => (double?)p.Rating.Value) ?? 0;
            var totalRates = _unitOFWork.ProposalRepo.GetAll().Count(p => p.WorkerId == proposal.WorkerId && p.Rating != null);

            var worker = _unitOFWork.WorkerRepo.GetById(proposal.WorkerId);
            worker.Rate = (float)Math.Round(avgRate, 1);
            worker.NumberOfRates = totalRates;
            _unitOFWork.Save();
            return Ok("Done");
        }


        




        [HttpPost("reactOnWorkerProject")]
        public ActionResult ReactOnWorkerProject(int ProjectId)
        {
            var Project = _unitOFWork.ProjectWorkerRepo.GetById(ProjectId);
            if (Project == null)
            {
                return NotFound();
            }
            Project.TotalReacts += 1;
            _unitOFWork.Save();
            return Ok(Project.TotalReacts);
        }
        [HttpPost("deleteReactOnWorkerProject")]
        public ActionResult DeleteReactOnWorkerProject(int ProjectId)
        {
            var Project = _unitOFWork.ProjectWorkerRepo.GetById(ProjectId);
            if (Project == null || Project.TotalReacts==0)
            {
                return NotFound();
            }
            Project.TotalReacts -= 1;
            _unitOFWork.Save();
            return Ok(Project.TotalReacts);
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var client = _unitOFWork.ClientRepo.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            _unitOFWork.ClientRepo.Delete(client);
            _unitOFWork.Save();
            return NoContent();
        }
        [HttpPost("Reports/ForWorker/{ClientID:alpha}")]
        public IActionResult Report(string ClientID, AddReportDTO ReportDTO)
        {
            Report report = _mapper.Map<Report>(ReportDTO);
            report.ClientId = ClientID;
            bool res = _unitOFWork.ClientRepo.ReportToWorker(ClientID, report);
            if (res)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
