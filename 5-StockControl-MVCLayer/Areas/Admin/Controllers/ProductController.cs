using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockControl_EntityLayer;
using StockControl_EntityLayer.Entities.Concrete;

namespace _5_StockControl_MVCLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        


        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string uri = "http://localhost:5051/api";
        public async Task<IActionResult> Index()
        {
            var response= await _httpClient.GetAsync($"{uri}/Product/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<List<Product>>();
                return View(product);
            }
            return NotFound();
        }

        public async Task<IActionResult> Create()
        {
            // İki API çağrısını aynı anda başlat
            var categoryTask = _httpClient.GetAsync($"{uri}/Category/GetAllActiveCategory");
            var supplierTask = _httpClient.GetAsync($"{uri}/Supplier/GetAllActiveSuppliers");

            // İki çağrının sonuçlarını bekle
            var categoryResponse = await categoryTask;
            var supplierResponse = await supplierTask;

            if (categoryResponse.IsSuccessStatusCode)
            {
                var categories = await categoryResponse.Content.ReadFromJsonAsync<List<Category>>();
                ViewBag.Categories = new SelectList(categories, "ID", "CategoryName");
            }

            if (supplierResponse.IsSuccessStatusCode)
            {
                var suppliers = await supplierResponse.Content.ReadFromJsonAsync<List<Supplier>>();
                ViewBag.Suppliers = new SelectList(suppliers, "ID", "SupplierName");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync($"{uri}/product/AddProduct", product);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = _httpClient.GetAsync($"{uri}/Product/GetProductById/{id}");
            var categoryTask =  _httpClient.GetAsync($"{uri}/Category/GetAllActiveCategory");
            var supplierTask =  _httpClient.GetAsync($"{uri}/Supplier/GetAllActiveSuppliers");

            var response1 = await response;
            var response2 = await categoryTask;
            var response3 = await supplierTask;

            if (response2.IsSuccessStatusCode)
            {
                var categories = await response2.Content.ReadFromJsonAsync<List<Category>>();
                ViewBag.Categories = new SelectList(categories, "ID", "CategoryName");
            }

            if (response3.IsSuccessStatusCode)
            {
                var suppliers = await response3.Content.ReadFromJsonAsync<List<Supplier>>();
                ViewBag.Suppliers = new SelectList(suppliers, "ID", "SupplierName");
            }
            if (response1.IsSuccessStatusCode)
            {
                var product = await response1.Content.ReadFromJsonAsync<Product>();
                return View(product);
            }
            return NotFound();  
        }

        [HttpPost]
        public async Task<IActionResult>Update(int id, Product product)
        {
            var response = await _httpClient.PostAsJsonAsync($"{uri}/Product/UpdateProduct/{id}", product);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }


        public async Task<IActionResult>Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/Product/DeleteProduct/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");   
            }
            return NotFound();
        }


        public async Task<IActionResult> MakeActive(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/Product/MakeActiveProduct/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

    }
}
