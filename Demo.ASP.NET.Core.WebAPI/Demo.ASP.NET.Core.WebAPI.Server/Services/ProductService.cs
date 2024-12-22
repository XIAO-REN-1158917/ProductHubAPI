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

        // Check if product name already exists
        public async Task<bool> IsProductNameTakenAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Product name cannot be null or empty.", nameof(name));
            }

            return await _productRepository.ExistsAsync(p => p.Name == name);
        }

        // Add a new product
        public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Product data cannot be null.");
            }

            if (string.IsNullOrEmpty(productDto.ProductName))
            {
                throw new ArgumentException("Product name cannot be null or empty.", nameof(productDto.ProductName));
            }

            if (string.IsNullOrEmpty(productDto.CategoryName))
            {
                throw new ArgumentException("Category name cannot be null or empty.", nameof(productDto.CategoryName));
            }

            // Validate if product name is unique
            if (await IsProductNameTakenAsync(productDto.ProductName))
            {
                throw new InvalidOperationException("Product name already exists.");
            }

            // Find the category by name
            var category = await _productRepository.GetCategoryByNameAsync(productDto.CategoryName);
            if (category == null)
            {
                throw new InvalidOperationException($"Category '{productDto.CategoryName}' does not exist.");
            }

            // Map DTO to Product entity
            var product = new Product
            {
                Name = productDto.ProductName,
                Category = category
            };

            // Save product to the database
            await _productRepository.AddAsync(product);

            // Return the created product as a DTO
            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.Name,
                CategoryName = product.Category?.Name
            };
        }

        // Get all products and convert to DTOs
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                ProductName = p.Name,
                CategoryName = p.Category?.Name
            });
        }

        // Get a product by ID and convert to DTO
        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.Name,
                CategoryName = product.Category?.Name
            };
        }
    }
}
