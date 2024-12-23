using Demo.ASP.NET.Core.WebAPI.Server.DTOs;

namespace Demo.ASP.NET.Core.WebAPI.Server.Validation
{
    public class DtoValidator
    {
        // validate - ProductCreateDto
        public static void ValidateProductCreateDto(ProductCreateDto productDto)
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
        }
    }
}
