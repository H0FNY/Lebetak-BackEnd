using AutoMapper;
using Lebetak.Services;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        UnitOFWork _unitOFWork;
        EmailService emailService = new EmailService();
        private readonly IMapper imapper;

        public AdminController(UnitOFWork unitOFWork, IMapper _imappe)
        {
            _unitOFWork = unitOFWork;
            imapper = _imappe;
        }


        [HttpGet("allCLients")]
        public IActionResult GetAllClients()
        {
            var clients = _unitOFWork.ClientRepo.GetAll();
            return Ok(clients);
        }

        [HttpGet("allOwners")]
        public IActionResult GetAllOwners()
        {
            var owners = _unitOFWork.OwnerRepo.GetAll();
            return Ok(owners);
        }

        [HttpGet("allWorkers")]
        public IActionResult GetAllWorkers()
        {
            var workers = _unitOFWork.WorkerRepo.GetAll();
            return Ok(workers);
        }

        [HttpGet("allPosts")]
        public IActionResult GetAllPosts()
        {
            var posts = _unitOFWork.PostRepo.GetAll();
            return Ok(posts);
        }

        [HttpGet("allProposals")]
        public IActionResult GetAllProposals()
        {
            var proposals = _unitOFWork.ProposalRepo.GetAll();
            return Ok(proposals);
        }
        [HttpGet("allServices")]
        public IActionResult GetAllServices()
        {
            var services = _unitOFWork.ServiceRepo.GetAll();
            return Ok(services);
        }
    }
}
