using _3_StockControl_ServiceLayer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockControl_EntityLayer;

namespace _4_StockControl_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericService<User> _service;

        public UserController(IGenericService<User> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            try
            {
                _service.Add(user);
                return Ok("Kullanıcı başarıyla eklendi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAllActiveUsers()
        {
            return Ok(_service.GetActive());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _service.GetById(id);
            if (user == null) return NotFound();
            else return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id,User user)
        {
            if (id!=user.ID) return BadRequest();
            try
            {
                _service.Update(user);
                return Ok("Kullanıcı başarıyla güncellendi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _service.GetById(id);
            if (user == null) return NotFound();
            try
            {
                _service.Remove(user);
                return Ok("Kullanıcı başrıyla silindi");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult MakeActiveUser(int id)
        {
            if (id == 0) return NotFound();
            else _service.GetActive(id);
            return Ok("Kullanıcı başarıyla aktif edildi");
        }


        [HttpGet("{email}/{password}")]
        public IActionResult Login(string email, string password)
        {
            var result = _service.Any(a => a.Email == email && a.Password == password);
            if (result)
            {
                User user = _service.GetByDefault(a => a.Email == email && a.Password == password);
                return Ok(user);
            }
            return NotFound(); 
            
        }

    }
}
