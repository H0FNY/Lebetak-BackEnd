using AutoMapper;
using Lebetak.DTOs;
using Lebetak.DTOs.Question;
using Lebetak.DTOs.Service;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        public UnitOFWork _unitOFWork { get; set; }
        IMapper _mapper;
        public ServiceController(UnitOFWork _unitOFWork, IMapper _mapper)
        {
            this._unitOFWork = _unitOFWork;
            this._mapper = _mapper;
        }
        [Authorize]
        [HttpGet("getAllForCompany")]
        public IActionResult GetAllForCompany()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Owner = _unitOFWork.OwnerRepo.GetById(userId);
            var services = _unitOFWork.ServiceRepo.GetAll().OrderByDescending(o => o.CreatedDate).Where(c=>c.CompanyId==Owner.Company.Id);
            return Ok(_mapper.Map<List<ServiceCardDTO>>(services));
        }

        [Authorize]
        [HttpGet("getAllForClient")]
        public IActionResult GetAllForClient()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Owner = _unitOFWork.OwnerRepo.GetById(userId);
            var services = _unitOFWork.ServiceRepo.GetAll().OrderByDescending(o=>o.CreatedDate).Where(c => c.ClientId == userId);
            return Ok(_mapper.Map<List<ServiceCardDTO>>(services));
        }

        [Authorize]
        [HttpGet("getById/{serviceId}")]
        public IActionResult GetServiceById(int serviceId)
        {
            var service = _unitOFWork.ServiceRepo.GetById(serviceId);
            return Ok(_mapper.Map<ServiceProfileDTO>(service));
        }

        
        [HttpGet("GetQuestionsForCategory/{categoryId}")]
        public IActionResult GetQuestionsForCategory(int categoryId)
        {
            var questions = _unitOFWork.QuestionRepo
                .Get(q => q.CategoryId == categoryId);

            if (!questions.Any())
                return NotFound();

            var result = new List<QuestionViewDTO>();

            foreach (var question in questions)
            {
                var res = new QuestionViewDTO
                {
                    Id = question.Id,
                    Text = question.Text,
                    answers = new List<OptionViewDTO>()
                };

                if (question.options != null)
                {
                    foreach (var option in question.options)
                    {
                        res.answers.Add(new OptionViewDTO
                        {
                            Id = option.Id,
                            Text = option.Text
                        });
                    }
                }

                result.Add(res);
            }

            return Ok(result);
        }


    }
}
