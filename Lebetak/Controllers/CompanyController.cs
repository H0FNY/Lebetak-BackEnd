using AutoMapper;
using Lebetak.DTOs;
using Lebetak.DTOs.ProjectWorker;
using Lebetak.DTOs.Question;
using Lebetak.DTOs.Worker;
using Lebetak.Models;
using Lebetak.Models.Attachments.Att_Worker_Project;
using Lebetak.Services;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly UnitOFWork _unitOFWork;
        IMapper _mapper;
        public CompanyController(UnitOFWork _unitOFWork,IMapper mapper)
        {
            this._unitOFWork = _unitOFWork;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public ActionResult GetAll()
        {
            return Ok(_unitOFWork.CompanyRepo.GetAll());
        }
        [HttpGet("getAllCards")]
        public ActionResult GetAllCards()
        {
            var companies = _unitOFWork.CompanyRepo.GetAll();
            return Ok(_mapper.Map<List<CompanyCardDTO>>(companies));
        }
        [HttpGet("get8Cards")]
        public ActionResult Get8Cards()
        {
            var companies = _unitOFWork.CompanyRepo.GetAll().Take(8);
            return Ok(_mapper.Map<List<CompanyCardDTO>>(companies));
        }
        [HttpGet("get-Company-profile/{CompanyId}")]
        public ActionResult GetWorkerProfile(int CompanyId)
        {
            var company = _unitOFWork.CompanyRepo.GetById(CompanyId);
            if (company == null) return NotFound();
            var res = _mapper.Map<CompanyProfileDTO>(company);
            return Ok(res);
        }
        [HttpGet("get-project-profile/{projectId}")]
        public ActionResult GetProjectProfile(int projectId)
        {
            var project = _unitOFWork.ProjectCompanyRepo.GetById(projectId);
            if (project == null) return NotFound();
            var res = _mapper.Map<ProjectProfileDTO>(project);
            return Ok(res);
        }

        [HttpGet("${id}")]
        public ActionResult GetById(int id)
        {
            var company = _unitOFWork.CompanyRepo.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
        [HttpGet("SearchByName/{name}")]
        public ActionResult GetByName(string name)
        {
            var companies = _unitOFWork.CompanyRepo.Get(c => c.Name.Contains(name));
            if (companies == null || !companies.Any())
            {
                return NotFound();
            }
            return Ok(companies);
        }

        [HttpPost("verify-company/{id}")]
        public ActionResult VerifyCompany(int id)
        {
            var company = _unitOFWork.CompanyRepo.GetById(id);
            if (company == null)
            {
                return NotFound("هذة الشركة غير موجودة");
            }
            company.IsVerified = true;
            _unitOFWork.Save();
            return Ok("تم توثيق هذة الشركة");
        }


        //[HttpPost("add-question")]
        //public ActionResult AddQuestions(QuestionAddDTO dto)
        //{
        //    if (dto == null)
        //        return BadRequest("Invalid data");

        //    if (dto.Options == null || dto.Options.Count == 0)
        //        return BadRequest("Question must have at least one option.");

        //    var question = new Question
        //    {
        //        Text = dto.Question,
        //        CompanyId = dto.CompanyId,
        //        options = dto.Options.Select(o => new Option
        //        {
        //            Text = o
        //        }).ToList(),
        //    };

        //    _unitOFWork.QuestionRepo.Add(question);
        //    _unitOFWork.Save();

        //    return Ok(new { message = "Question created successfully", questionId = question.Id });
        //}

        [HttpPost("add-project")]
        public async Task<IActionResult> AddProject(ProjectAddDTO dto)
        {
            var ownerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _unitOFWork.OwnerRepo.GetById(ownerUserId);
            var Company = _unitOFWork.CompanyRepo.GetById(owner.CompanyId);
            if (Company == null)
            {
                return NotFound("Company not found");
            }
            ProjectCompany project = _mapper.Map<ProjectCompany>(dto);
            _unitOFWork.ProjectCompanyRepo.Add(project);
            _unitOFWork.Save();
            project.Images = new List<AttCompanyProject>();
            if (dto.Files != null)
            {
                foreach (var file in dto.Files)
                {
                    var f = await FileUpload.UploadAsync(file, _unitOFWork.Cloudinary, "Projects");
                    string url = f.Url.ToString();
                    project.Images.Add(new AttCompanyProject { URL = url, ProjectId = project.Id });

                }

            }
            _unitOFWork.Save();
            return Ok("Project Created Successfully");
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var company = _unitOFWork.CompanyRepo.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            _unitOFWork.CompanyRepo.Delete(company);
            _unitOFWork.Save();
            return NoContent();
        }
    }
}
