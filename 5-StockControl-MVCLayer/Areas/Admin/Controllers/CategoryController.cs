using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockControl_EntityLayer.Entities.Concrete;

namespace _5_StockControl_MVCLayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;
        
        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string uri = "http://localhost:5051/api/Category";
        public async Task<IActionResult> Index()
        {
            var response= await _httpClient.GetAsync($"{uri}/GetAllCategories");
            if (response.IsSuccessStatusCode)
            {
                var categories= await response.Content.ReadFromJsonAsync<List<Category>>();
                return View(categories);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            
            category.IsActive = true;
            var response = await _httpClient.GetAsync($"{uri}/AddCategory");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/GetCategoryById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var category = await response.Content.ReadFromJsonAsync<Category>();
                return View(category);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult>Update(int id, Category category)
        {
            category.ModifiedDate = DateTime.Now;
            var response = await _httpClient.PutAsJsonAsync($"{uri}/UpdateCategory/{id}", category);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult>Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{uri}/DeleteCategory/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public async Task<IActionResult> MakeActive(int id)
        {
            var response = await _httpClient.GetAsync($"{uri}/MakeActiveCategory/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
