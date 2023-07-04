using Microsoft.AspNetCore.Mvc;
using SudelMerck.Webfrontend.Services.ProductService;

namespace SudelMerck.Webfrontend.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProduct();
            return View(products);
        }

        public IActionResult Details(Guid Id)
        {
            var product = _productService.GetProductById(Id);
            return View(product);

        }



    }
}
