using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        public UnitOFWork _unitOfWork { get; set; }
        public OwnerController(UnitOFWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        [HttpGet("get-all")]
        public ActionResult GetAll()
        {
            return Ok(_unitOfWork.OwnerRepo.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            var owner = _unitOfWork.OwnerRepo.GetById(id);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var owner = _unitOfWork.OwnerRepo.GetById(id);
            if (owner == null)
            {
                return NotFound();
            }
            _unitOfWork.OwnerRepo.Delete(owner);
            _unitOfWork.Save();
            return Ok("Owner was Deleted");
        }

    }
}
