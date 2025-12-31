using AutoMapper;
using Lebetak.DTOs;
using Lebetak.DTOs.ProjectWorker;
using Lebetak.DTOs.Worker;
using Lebetak.Models;
using Lebetak.Models.Attachments.Att_Worker_Project;
using Lebetak.Services;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly UnitOFWork _unitOfWork;
        IMapper _mapper;

        public WorkerController(UnitOFWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public ActionResult GetAll()
        {
            var workers = _unitOfWork.WorkerRepo.GetAll();

            return Ok(workers);
        }
        [HttpGet("getAllCards")]
        public ActionResult GetAllCards()
        {
            var workers = _unitOfWork.WorkerRepo.GetAll();
            
            return Ok(_mapper.Map<List<WorkerCardsDTO>>(workers));
        }
        [HttpGet("get8Cards")]
        public ActionResult Get8Cards()
        {
            var workers = _unitOfWork.WorkerRepo.GetAll().Take(8);
            return Ok(_mapper.Map<List<WorkerCardsDTO>>(workers));
        }
        [HttpGet("get-worker-profile/{userId}")]
        public ActionResult GetWorkerProfile(string userId)
        {
            var worker = _unitOfWork.WorkerRepo.GetById(userId);
            if (worker == null) return NotFound();
            var res = _mapper.Map<WorkerProfileDTO>(worker);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("myProfile")]
        public ActionResult GetMyProfile()
        {
            var workerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var worker = _unitOfWork.WorkerRepo.GetById(workerUserId);
            if (worker == null) return NotFound();
            var res = _mapper.Map<MyProfileWorker>(worker);
            return Ok(res);
        }

        [HttpGet("get-project-profile/{projectId}")]
        public ActionResult GetProjectProfile(int projectId)
        {
            var project = _unitOfWork.ProjectWorkerRepo.GetById(projectId);
            if (project == null) return NotFound();
            var res = _mapper.Map<ProjectProfileDTO>(project);
            return Ok(res);
        }

        [HttpGet("GetLocations")]
        public ActionResult GetLocations()
        {
            var workers = _unitOfWork.WorkerRepo.GetAll();

            return Ok(_mapper.Map<List<WorkerMapDTO>>(workers));
        }


        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            var worker = _unitOfWork.WorkerRepo.GetById(id);
            if (worker == null)
            {
                return NotFound();
            }
            return Ok(worker);
        }
        [HttpGet("SearchByName/{name}")]
        public ActionResult GetByName(string name)
        {
            var workers = _unitOfWork.WorkerRepo.Get(c => c.User.F_Name.Contains(name) || c.User.L_Name.Contains(name));
            if (workers == null || !workers.Any())
            {
                return NotFound();
            }
            return Ok(workers);
        }
        
        [HttpPut("update-worker-info/{userId}")]
        public IActionResult UpdateWorkerInfo(string userId, [FromBody] UpdateWorkerInfoDTO dto)
        {

            var worker = _unitOfWork.WorkerRepo.GetById(userId);
            if (worker == null) return NotFound();

            if (dto.Description != null) worker.Description = dto.Description;
            if (dto.ExperienceYears != null) worker.ExperienceYears = dto.ExperienceYears.Value;
            if (dto.HourlyPrice != null) worker.HourlyPrice = dto.HourlyPrice.Value;
            if (dto.CategoryId != null) worker.CategoryId = dto.CategoryId.Value;

            // update skills
            if (dto.Skills != null)
            {
                worker.WorkerSkills = dto.Skills
                    .Select(s => new WorkerSkills { SkillId = s, WorkerId = userId })
                    .ToList();
            }

            _unitOfWork.WorkerRepo.Update(worker);
            _unitOfWork.Save();

            return Ok(worker);
        }

        [HttpGet("get-worker-byId")]
        public IActionResult ShowWorker(string userId)
        {
            var worker = _unitOfWork.WorkerRepo.GetById(userId);
            var user = _unitOfWork.UserRepo.GetById(userId);
            if (worker == null) return NotFound();
            if (user == null) return NotFound();
            var res = _mapper.Map<WorkerCardsDTO>(worker);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("add-project")]
        public async Task<IActionResult> AddProject(ProjectAddDTO dto)
        {
            var workerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var worker = _unitOfWork.WorkerRepo.GetById(workerUserId);
            if (worker == null) return NotFound();
            ProjectWorker project = _mapper.Map<ProjectWorker>(dto);
            project.WorkerId = workerUserId;
            _unitOfWork.ProjectWorkerRepo.Add(project);
            _unitOfWork.Save();
            project.Images = new List<AttWorkerProject>();
            if (dto.Files != null)
            {
                foreach (var file in dto.Files)
                {
                    var f = await FileUpload.UploadAsync(file, _unitOfWork.Cloudinary, "Projects");
                    string url = f.Url.ToString();
                    project.Images.Add(new AttWorkerProject { URL = url, ProjectId = project.Id });

                }
            }
            _unitOfWork.Save();
            return Ok("Project Created Successfully");
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var worker = _unitOfWork.WorkerRepo.GetById(id);
            if (worker == null)
            {
                return NotFound();
            }
            _unitOfWork.WorkerRepo.Delete(worker);
            _unitOfWork.Save();
            return Ok("Worker was deleted");
        }

        //Search 
        [HttpGet("search/{categoryId:int}")]
        public ActionResult SearchByDepartmentID(int categoryId)
        {
            Category category = _unitOfWork.CategoryRepo.GetById(categoryId);
            if(category != null)
            {
                IQueryable<Worker> Workers =
                    _unitOfWork.WorkerRepo.SearchWorkerByCategoryID(categoryId);
                return Ok(Workers);
            }
            return NotFound();
        }
        
        [HttpGet("search/{LPrice:int}/{HPrice:int}")]
        public ActionResult SearchByHourlyPrice(int LPrice , int HPrice)
        {
                IQueryable<Worker> Workers =
                    _unitOfWork.WorkerRepo.SearchWorkerByHourlyPrice(LPrice,HPrice);
                return Ok(Workers);
        }

    }
}
