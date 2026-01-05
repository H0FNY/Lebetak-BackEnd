using AutoMapper;
using CloudinaryDotNet.Actions;
using Lebetak.Models;
using Lebetak.Services;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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


        [HttpGet("allUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _unitOFWork.AppUserRepo.GetAll();

            var res = users.Select(u => new
            {
                id = u.Id,
                name = u.F_Name + " " + u.L_Name,
                userName= u.UserName,
                email = u.Email,
                phone = u.PhoneNumber,
                role =
                    u.Client != null ? "Client" :
                    u.Owner != null ? "Owner" :
                    u.Worker != null ? "Worker" :
                    "Admin"
            });
            return Ok(res);
        }


        [HttpGet("allPosts")]
        public IActionResult GetAllPosts()
        {
            var posts = _unitOFWork.PostRepo.GetAll();
            var res = posts.Select(p => new
            {
                id = p.Id,
                title = p.Title,
                userName = p.Client.User.UserName,
                createdDate = p.CreatedDate,
                status= p.Status.ToString(),
            });
            return Ok(res);
        }

        [HttpGet("allProposals")]
        public IActionResult GetAllProposals()
        {
            var proposals = _unitOFWork.ProposalRepo.GetAll();
            var res = proposals.Select(p => new
            {
                id = p.Id,
                client = p.Post.Client.User.UserName,
                title = p.Description,
                CreatedAt = p.Created_At,
                status = p.Status.ToString(),
            });
            return Ok(res);
        }
        [HttpGet("allServices")]
        public IActionResult GetAllServices()
        {
            var services = _unitOFWork.ServiceRepo.GetAll();
            var res = services.Select(p => new
            {
                id = p.Id,
                title = p.Description,
                Client = p.Client.User.UserName,
                CreatedDate = p.CreatedDate,
                Company= p.Company.Name,
            });
            return Ok(res);
        }
    }
}
