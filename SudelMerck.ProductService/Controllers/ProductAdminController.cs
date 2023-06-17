﻿using Microsoft.AspNetCore.Mvc;
using SudelMerck.ProductService.Model.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SudelMerck.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAdminController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductAdminController(IProductService productService)
        {
            _productService = productService;
        }
        // POST api/<ProductAdminController>
        [HttpPost]
        public IActionResult Post([FromBody] AddNewProductDto product)
        {
            _productService.AddNewProductDto(product);
            return Ok();
        }

        
    }
}
