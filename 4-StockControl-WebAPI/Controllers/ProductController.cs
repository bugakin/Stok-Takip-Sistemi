using _3_StockControl_ServiceLayer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockControl_EntityLayer;

namespace _4_StockControl_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> _service;

        public ProductController(IGenericService<Product> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_service.GetAll(a => a.Category, b => b.Supplier));
        }

        //tüm aktif ürünleri getir
        [HttpGet]
        public IActionResult GetActiveProducts()
        {
            return Ok(_service.GetActive(a => a.Category, b => b.Supplier));
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            return Ok(_service.GetById(id));  
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                _service.Add(product);
                return Ok("Ürün başarıyla eklendi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id,Product product)
        {
        
            try
            {
                if (id != product.ID) return BadRequest();
                _service.Update(product);
                return Ok("Ürün başarıyla güncellendi");
            }
            catch (Exception)
            {
                return BadRequest();
                
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id) 
        {
             Product product = _service.GetById(id);
            if (product == null) return NotFound();
            try
            {
                _service.Remove(product);
                return Ok("Ürün başarıyla silindi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public IActionResult MakeActiveProduct(int id)
        {
            Product product= _service.GetById(id);
            if(product == null) return NotFound();
            try
            {
                _service.GetActive(id);
                return Ok("Ürün başarıyla aktif edildi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }
}
