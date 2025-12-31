using AutoMapper;
using Lebetak.Common.Enumes;
using Lebetak.DTOs;
using Lebetak.DTOs.Urgency;
using Lebetak.Hubs;
using Lebetak.Models;
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
    public class PostController : ControllerBase
    {
        private readonly UnitOFWork _unitOFWork;
        private readonly IHubContext<Newshub> _hub;
        IMapper _mapper;

        public PostController(UnitOFWork unit, IHubContext<Newshub> hub,IMapper mapper)
        {
            _unitOFWork = unit;
            _hub = hub;
            _mapper = mapper;
        }


        [HttpGet("getAll")]
        public IActionResult GetAllPosts()
        {
            var posts = _unitOFWork.PostRepo.GetAll().OrderByDescending(b => b.CreatedDate).Where(i=>i.Status==JobStatus.Open);
            return Ok(_mapper.Map<List<PostCardDTO>>(posts));
        }

        [Authorize]
        [HttpGet("getMyPosts")]
        public IActionResult GetMyPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts = _unitOFWork.PostRepo.GetAll().OrderByDescending(b=>b.CreatedDate).Where(i => i.ClientId==userId);
            
            return Ok(_mapper.Map<List<PostCardDTO>>(posts));
        }


        [HttpGet("/api/Urgency/getAll")]
        public IActionResult GetUrgencies()
        {
            var urgencies = _unitOFWork.UrgencyRepo.GetAll();

            return Ok(_mapper.Map<List<UrgencyViewDTO>>(urgencies));
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            var post = _unitOFWork.PostRepo.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PostViewDTO>(post));
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            Post post = _unitOFWork.PostRepo.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            post.Status = JobStatus.Cancelled;
            _unitOFWork.Save();
            return NoContent();
        }


    }
}


// Front end Signal R 
/*
 <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/7.0.5/signalr.min.js"></script>

<script>
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/newsHub")
    .build();

connection.on("ReceiveNotification", function (message) {
    alert(message);
});

connection.start();
</script>

 */