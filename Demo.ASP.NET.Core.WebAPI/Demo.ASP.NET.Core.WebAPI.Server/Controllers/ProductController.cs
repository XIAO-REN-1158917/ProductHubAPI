using Demo.ASP.NET.Core.WebAPI.Server.DTOs;
using Demo.ASP.NET.Core.WebAPI.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ASP.NET.Core.WebAPI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // Check in real-time if the product name already exists.
        [HttpGet("check-name")]
        public async Task<IActionResult> CheckProductName([FromQuery] string name)
        {
            var exists = await _productService.IsProductNameTakenAsync(name);
            return Ok(new { IsTaken = exists });
        }
    }
}
