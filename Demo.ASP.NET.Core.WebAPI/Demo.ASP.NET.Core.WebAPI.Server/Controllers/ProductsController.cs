using Demo.ASP.NET.Core.WebAPI.Server.Common;
using Demo.ASP.NET.Core.WebAPI.Server.DTOs;
using Demo.ASP.NET.Core.WebAPI.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ASP.NET.Core.WebAPI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResponseDto>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            //The use of an independently encapsulated response format is intended to
            //demonstrate my understanding of alternative practices in commercial development and team collaboration.
            //(The data in the demo itself is very simple and does not necessarily require an independently encapsulated format.)
            //var response = new ApiResponse<IEnumerable<ProductResponseDto>>(
            //    true,
            //    "Successfully retrieved products",
            //    products,
            //    new { totalItems = products.Count() }
            //);
            //return Ok(response);

            return products;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            
            if (product == null)
                throw new InvalidOperationException();

            return Ok(product);
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
            var productResponse = await _productService.AddProductAsync(productDto);

            //return CreatedAtAction(
            //    nameof(GetProductById),
            //    new { id = productResponse.Id },
            //    new ApiResponse<ProductResponseDto>(
            //        true,
            //        "Product successfully created",
            //        productResponse
            //        )
            //    );
            //return the created object
            return Ok(productResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductCreateDto productDto)
        {
            var productResponse = await _productService.UpdateProductAsync(id, productDto);

            //return Ok(new ApiResponse<ProductResponseDto>(
            //        true,
            //        "Product successfully updated",
            //        productResponse
            //        )
            //    );

            return Ok(productResponse);
        }

        // YAGNI: You Ain't Gonna Need It
        // Code is debt 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
