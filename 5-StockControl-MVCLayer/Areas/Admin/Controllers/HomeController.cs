using Microsoft.AspNetCore.Mvc;

namespace _5_StockControl_MVCLayer.Areas.AdminArea.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
