using _3_StockControl_ServiceLayer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockControl_EntityLayer.Entities.Concrete;

namespace _4_StockControl_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericService<Category> _service;

        public CategoryController(IGenericService<Category> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _service.Add(category);
            return Ok("Kategori başarıyla eklendi");
        }
        [HttpGet]
        public IActionResult GetAllActiveCategory()
        {

            return Ok(_service.GetActive());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            if (id != category.ID) return BadRequest();
            try
            {
                if (_service.Update(category)) return Ok("Kategori başarıyla güncellendi");
                else return BadRequest();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _service.GetById(id);
            if (category == null) return NotFound();
            else _service.Remove(category);
            return Ok("Kategori başarıyla silindi");
        }

        [HttpGet("{id}")]
        public IActionResult MakeActiveCategory(int id)
        {
            var category = _service.GetById(id);
            if(category == null) return NotFound();
            try
            {
                _service.GetActive(id);
                return Ok("Kategori başarıyla aktif edildi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }



    }
}
