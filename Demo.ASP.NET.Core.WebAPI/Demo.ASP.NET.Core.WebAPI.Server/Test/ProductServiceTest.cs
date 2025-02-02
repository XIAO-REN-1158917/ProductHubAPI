//using System.Linq.Expressions;
//using Xunit;
//using Moq;
//using Demo.ASP.NET.Core.WebAPI.Server.Services;
//using Demo.ASP.NET.Core.WebAPI.Server.Repositories;
//using Demo.ASP.NET.Core.WebAPI.Server.DTOs;
//using Demo.ASP.NET.Core.WebAPI.Server.Models;

//namespace Demo.ASP.NET.Core.WebAPI.Server.Test
//{
//    public class ProductServiceTest
//    {
//        private readonly Mock<IProductRepository> _mockRepository;
//        private readonly IProductService _productService;

//        public ProductServiceTest()
//        {
//            // Mock the IProductRepository interface
//            _mockRepository = new Mock<IProductRepository>();

//            // Inject the mock repository into ProductService
//            _productService = new IProductService(_mockRepository.Object);
//        }

//        [Fact]
//        public async Task GetAllProductsAsync_ReturnsListOfProducts()
//        {
//            // Arrange
//            var mockProducts = new List<Product>
//            {
//                new Product { Id = 1, Name = "Laptop", Category = new Category { Name = "Electronics" } },
//                new Product { Id = 2, Name = "Apple", Category = new Category { Name = "Groceries" } }
//            };
//            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockProducts);

//            // Act
//            var result = await _productService.GetAllProductsAsync();

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(2, result.Count());
//            Assert.Equal("Laptop", result.First().ProductName);
//            Assert.Equal("Electronics", result.First().CategoryName);
//        }

//        [Fact]
//        public async Task GetProductByIdAsync_ProductExists_ReturnsProduct()
//        {
//            // Arrange
//            var product = new Product { Id = 1, Name = "Laptop", Category = new Category { Name = "Electronics" } };
//            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

//            // Act
//            var result = await _productService.GetProductByIdAsync(1);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("Laptop", result.ProductName);
//            Assert.Equal("Electronics", result.CategoryName);
//        }

//        [Fact]
//        public async Task GetProductByIdAsync_ProductDoesNotExist_ReturnsNull()
//        {
//            // Arrange
//            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product?)null);

//            // Act
//            var result = await _productService.GetProductByIdAsync(1);

//            // Assert
//            Assert.Null(result);
//        }

//        [Fact]
//        public async Task AddProductAsync_ProductNameAlreadyExists_ThrowsException()
//        {
//            // Arrange
//            var productDto = new ProductCreateDto { ProductName = "Laptop", CategoryName = "Electronics" };

//            _mockRepository.Setup(repo => repo.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(true);

//            // Act & Assert
//            await Assert.ThrowsAsync<InvalidOperationException>(() => _productService.AddProductAsync(productDto));
//        }

//        [Fact]
//        public async Task AddProductAsync_ValidProduct_ReturnsAddedProduct()
//        {
//            // Arrange
//            var productDto = new ProductCreateDto { ProductName = "Laptop", CategoryName = "Electronics" };
//            var category = new Category { Id = 1, Name = "Electronics" };

//            _mockRepository.Setup(repo => repo.ExistsAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(false);
//            _mockRepository.Setup(repo => repo.GetCategoryByNameAsync("Electronics")).ReturnsAsync(category);
//            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()));

//            // Act
//            var result = await _productService.AddProductAsync(productDto);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("Laptop", result.ProductName);
//            Assert.Equal("Electronics", result.CategoryName);
//        }

//        [Fact]
//        public async Task DeleteProductAsync_ProductDoesNotExist_ThrowsException()
//        {
//            // Arrange
//            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product?)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<InvalidOperationException>(() => _productService.DeleteProductAsync(1));
//        }

//        [Fact]
//        public async Task DeleteProductAsync_ProductExists_DeletesProduct()
//        {
//            // Arrange
//            var product = new Product { Id = 1, Name = "Laptop", Category = new Category { Name = "Electronics" } };
//            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);
//            _mockRepository.Setup(repo => repo.DeleteAsync(1));

//            // Act
//            await _productService.DeleteProductAsync(1);

//            // Assert
//            _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
//        }

//        [Fact]
//        public async Task UpdateProductAsync_ProductDoesNotExist_ThrowsException()
//        {
//            // Arrange
//            var productDto = new ProductCreateDto { ProductName = "Laptop", CategoryName = "Electronics" };
//            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product?)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<InvalidOperationException>(() => _productService.UpdateProductAsync(1, productDto));
//        }

//        [Fact]
//        public async Task UpdateProductAsync_ValidProduct_UpdatesAndReturnsProduct()
//        {
//            // Arrange
//            var product = new Product { Id = 1, Name = "Old Laptop", Category = new Category { Name = "Old Electronics" } };
//            var productDto = new ProductCreateDto { ProductName = "New Laptop", CategoryName = "Electronics" };
//            var category = new Category { Name = "Electronics" };

//            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);
//            _mockRepository.Setup(repo => repo.GetCategoryByNameAsync("Electronics")).ReturnsAsync(category);
//            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()));

//            // Act
//            var result = await _productService.UpdateProductAsync(1, productDto);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("New Laptop", result.ProductName);
//            Assert.Equal("Electronics", result.CategoryName);
//        }
//    }
//}
