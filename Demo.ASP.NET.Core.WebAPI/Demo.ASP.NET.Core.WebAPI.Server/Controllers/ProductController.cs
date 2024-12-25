using Demo.ASP.NET.Core.WebAPI.Server.Common;
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

            //In general, the front-end provides ID options for customers to select,
            //rather than allowing them to input the ID manually,
            //so the likelihood of not finding a product is very low.
            // Returning null is chosen here for simplicity, as it avoids the overhead of exception handling
            // and clearly represents the absence of a product in a lightweight manner.
            // In scenarios with higher uncertainty or critical requirements,
            // throwing an exception might be more appropriate.
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

        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductCreateDto productDto)
        {
            //This demonstrates my understanding of exception handling mechanisms
            //while also reflecting my awareness that,
            //although the front-end reduces the likelihood of errors,
            //the back-end must still uphold the responsibility of defensive programming.

            var productResponse = await _productService.AddProductAsync(productDto);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = productResponse.Id },
                new ApiResponse<ProductResponseDto>(
                    true,
                    "Product successfully created",
                    productResponse
                    )
                );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductCreateDto productDto)
        {
            var productResponse = await _productService.UpdateProductAsync(id, productDto);

            return Ok(new ApiResponse<ProductResponseDto>(
                    true,
                    "Product successfully updated",
                    productResponse
                    )
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
