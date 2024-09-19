using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockControl_EntityLayer;
using StockControl_EntityLayer.Entities.Concrete;

namespace _5_StockControl_MVCLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly HttpClient _httpClient;

        public SupplierController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string uri = "http://localhost:5051/api/Supplier";

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{uri}/GetAllSuppliers");
            if (response.IsSuccessStatusCode)
            {
                var supplier = await response.Content.ReadFromJsonAsync<List<Supplier>>();
                return View(supplier);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {

            supplier.IsActive = true;
            var response = await _httpClient.GetAsync($"{uri}/AddSupplier");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetSupplierById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var supplier = await response.Content.ReadFromJsonAsync<Supplier>();
                return View(supplier);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, Supplier supplier)
        {
            supplier.ModifiedDate = DateTime.Now;
            var response = await _httpClient.PutAsJsonAsync($"{uri}/UpdateSupplier/{id}", supplier);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/DeleteSupplier/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> MakeActive(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/MakeActiveSupplier/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
