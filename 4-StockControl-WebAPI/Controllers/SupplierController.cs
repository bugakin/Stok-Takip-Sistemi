using _3_StockControl_ServiceLayer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockControl_EntityLayer;

namespace _4_StockControl_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IGenericService<Supplier> _service;

        public SupplierController(IGenericService<Supplier> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult AddSupplier(Supplier supplier)
        {
            try
            {
                _service.Add(supplier);
                return Ok("Kategori başarıyla eklendi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAllActiveSuppliers()
        {
            return Ok(_service.GetActive());
        }

        [HttpGet("{id}")]
        public IActionResult GetSupplierById(int id)
        {
            return Ok(_service.GetById(id));
        }


        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, Supplier supplier)
        {
            if (id != supplier.ID) return BadRequest();
            try
            {
                _service.Update(supplier);
                return Ok("Tedarikçi başarıyla güncellendi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            var supplier= _service.GetById(id); 
            if (supplier == null) return NotFound();
            try
            {
               _service.Remove(supplier);
                return Ok("Tedarikçi başrıyla silindi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult MakeActiveSupplier(int id)
        {
            if(id==0) return NotFound();    
            else _service.GetActive(id);
            return Ok("Tedarikçi başarıyla aktif edildi");
        }
    }
}
