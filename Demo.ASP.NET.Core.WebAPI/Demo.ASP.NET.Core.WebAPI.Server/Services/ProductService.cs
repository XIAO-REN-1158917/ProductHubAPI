using Demo.ASP.NET.Core.WebAPI.Server.DTOs;
using Demo.ASP.NET.Core.WebAPI.Server.Models;
using Demo.ASP.NET.Core.WebAPI.Server.Repositories;

namespace Demo.ASP.NET.Core.WebAPI.Server.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Get all product and convert to DTO
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                ProductName = p.Name,
                CategoryName = p.Category?.Name
            });
        }

        // Get a product by ID and convert to DTO
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                ProductName = product.Name,
                CategoryName = product.Category?.Name
            };
        }
    }
}
