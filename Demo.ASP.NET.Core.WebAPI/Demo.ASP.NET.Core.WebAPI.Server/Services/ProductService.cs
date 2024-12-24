using Demo.ASP.NET.Core.WebAPI.Server.DTOs;
using Demo.ASP.NET.Core.WebAPI.Server.Models;
using Demo.ASP.NET.Core.WebAPI.Server.Repositories;
using Demo.ASP.NET.Core.WebAPI.Server.Validation;

namespace Demo.ASP.NET.Core.WebAPI.Server.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Get all products and convert to DTOs
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(MapToResponseDto);
        }

        // Get a product by ID and convert to DTO
        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            // Map entity to response DTO
            return MapToResponseDto(product);
        }

        // Check if product name already exists
        public async Task<bool> IsProductNameTakenAsync(string name)
        {
            // Logically, non-null validation is needed here, but EF Core and ASP.NET Core have built-in automatic validation mechanisms. 
            // For simple validations, such as non-null checks, leveraging these mechanisms reduces redundancy and improves maintainability.
            // For more complex validation logic that depends on business rules or database queries, 
            // it can be implemented in the service layer with error handling managed through middleware for consistency.
            //if (string.IsNullOrEmpty(name))
            //{
            //    throw new ArgumentException("Product name cannot be null or empty.", nameof(name));
            //}

            return await _productRepository.ExistsAsync(p => p.Name == name);
        }

        // Add a new product
        public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto productDto)
        {
            // Validate input
            DtoValidator.ValidateProductCreateDto(productDto);

            // Validate if product name is unique
            if (await IsProductNameTakenAsync(productDto.ProductName))
            {
                throw new InvalidOperationException("Product name already exists.");
            }

            // Find the category
            var category = await GetCategoryOrThrowAsync(productDto.CategoryName);

            // Map DTO to Product entity
            var product = new Product
            {
                Name = productDto.ProductName,
                Category = category
            };

            // Save product to the database
            await _productRepository.AddAsync(product);

            // Map entity to response DTO
            return MapToResponseDto(product);
        }

        //Update an existing product
        public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductCreateDto productDto)
        {
            // Validate input
            DtoValidator.ValidateProductCreateDto(productDto);

            // Find the product to update
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {id} does not exist.");
            }

            // Validate if product name is unique (exclude the current product)
            if (product.Name != productDto.ProductName && await IsProductNameTakenAsync(productDto.ProductName))
            {
                throw new InvalidOperationException("Product name already exists.");
            }

            // Find the category
            var category = await GetCategoryOrThrowAsync(productDto.CategoryName);

            // Map DTO to Product entity
            product.Name = productDto.ProductName;
            product.Category = category;

            // Save changes to the database
            await _productRepository.UpdateAsync(product);

            // Map entity to response DTO
            return MapToResponseDto(product);
        }

        public async Task DeleteProductAsync(int id)
        {

            // Find the product to delete
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {id} does not exist.");
            }

            await _productRepository.DeleteAsync(id);
        }
        

        //Encapsulate these methods
        //to maintain the simplicity of the main logic in the Service and facilitate reuse.
        private async Task<Category> GetCategoryOrThrowAsync(string categoryName)
        {
            var category = await _productRepository.GetCategoryByNameAsync(categoryName);
            if (category == null)
            {
                throw new InvalidOperationException($"Category '{categoryName}' does not exist.");
            }
            return category;
        }

        private ProductResponseDto MapToResponseDto(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.Name,
                CategoryName = product.Category.Name 
            };
        }

    }
}
