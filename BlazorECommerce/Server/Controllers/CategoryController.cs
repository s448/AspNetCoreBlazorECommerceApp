using BlazorECommerce.Server.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }
    }
}
