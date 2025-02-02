using Demo.ASP.NET.Core.WebAPI.Server.DTOs;

namespace Demo.ASP.NET.Core.WebAPI.Server.Services
{
    public interface IProductService
    {
        Task<ProductResponseDto> AddProductAsync(ProductCreateDto productDto);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto?> GetProductByIdAsync(int id);
        Task<bool> IsProductNameTakenAsync(string name);
        Task<ProductResponseDto> UpdateProductAsync(int id, ProductCreateDto productDto);
    }
}