
using AutoMapper;
using Lebetak.Common;
using Lebetak.DTOs;
using Lebetak.Models;
using Lebetak.Profiles;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly UnitOFWork unit;
        private readonly IMapper mapper;
        public CategoriesController(UnitOFWork _unit, IMapper _mapper)
        {
            unit = _unit;
            mapper = _mapper;
        }

        //Get All Category
        [HttpGet("getAll")]
        public IActionResult GetAllCategory()
        {

            var categories = unit.CategoryRepo.GetAll().ToList();
            var result = mapper.Map<List<CategoryViewDTO>>(categories);

            return Ok(result);
        }

        // Add Cateegory 
        [HttpPost]
        public IActionResult Add(AddCategoryDto dto)
        {
            var cat = mapper.Map<Category>(dto);
            unit.CategoryRepo.Add(cat);
            unit.Save();
            return Ok(mapper.Map<CategoryViewDTO>(cat));
        }

        //Update

        [HttpPut]
        public IActionResult Update(UpdateCategoryDto dto)
        {
            var cat = unit.CategoryRepo.GetById(dto.Id);
            if (cat == null) return NotFound("Category not found");

            mapper.Map(dto, cat);
            unit.CategoryRepo.Update(cat);
            unit.Save();

            return Ok(mapper.Map<CategoryViewDTO>(cat));
        }


        //Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cat = unit.CategoryRepo.GetById(id);
            if (cat == null) return NotFound("Category not found");

            unit.CategoryRepo.Delete(cat);
            unit.Save();

            return Ok();
        }

    }
}
