using Microsoft.AspNetCore.Mvc;
using SudelMerck.ProductService.Model.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SudelMerck.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = _categoryService.GetCategories();
            return Ok(data);
        }


        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] CategotyDto categotyDto)
        {
            _categoryService.AddCategory(categotyDto);
            return Ok();
        }
    }
}
