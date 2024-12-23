using Demo.ASP.NET.Core.WebAPI.Server.Common;
using Demo.ASP.NET.Core.WebAPI.Server.DTOs;
using Demo.ASP.NET.Core.WebAPI.Server.Services;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            //The use of an independently encapsulated response format is to
            //demonstrate my understanding of typical practices in commercial development and team collaboration.
            //(The data in the demo itself is very simple and does not necessarily require an independently encapsulated format.)
            var response = new ApiResponse<IEnumerable<ProductResponseDto>>(
            true,
            "Successfully retrieved products",
            products,
            new { totalItems = products.Count() }
            );
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                //For some simple responses, using a full format is unnecessary (to reduce code redundancy.)
                //However, the final decision depends on the team's requirements.
                return NotFound(new { message = "Product not found", productId = id });
            }

            return Ok(new ApiResponse<ProductResponseDto>(
                true,
                "Successfully retrieved the product",
                product
            ));
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
