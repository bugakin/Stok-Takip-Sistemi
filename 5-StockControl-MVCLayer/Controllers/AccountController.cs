using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StockControl_EntityLayer;
using System.Security.Claims;

namespace _5_StockControl_MVCLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string uri = "http://localhost:5051/api/User";

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var response = await _httpClient.GetAsync($"{uri}/Login/{user.Email}/{user.Password}");
            if (response.IsSuccessStatusCode)
            {
                var userx=await response.Content.ReadFromJsonAsync<User>();

                List<Claim> claims = new List<Claim>()//kullanıcının kimliğini oluşutrduk
                {
                    new Claim(ClaimTypes.Name, userx.FirstName),//her claim kullanıcının bir özelliğini taşır
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
                   

                };


                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //claims lisetsi kullanarak bir kimlik oluşturduk ve cookie tabanlı doğrulama şeması ile ilişkilendirdik

                AuthenticationProperties properties = new AuthenticationProperties()//oturum bilgilerini tuttuk burada
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddMinutes(5)
                     
                };


                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                //burada kimlik oluşturduk

                await HttpContext.SignInAsync(principal, properties);

                return RedirectToAction("Index", "Home", new {area="Admin"});

            }
            return View(user);
        }
    }
}
