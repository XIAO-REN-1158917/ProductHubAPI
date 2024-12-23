using Demo.ASP.NET.Core.WebAPI.Server.DTOs;

namespace Demo.ASP.NET.Core.WebAPI.Server.Validation
{
    public class DtoValidator
    {
        /// <summary>
        /// This utility class centralises validation logic for DTOs, promoting code reuse 
        /// and separation of concerns. By encapsulating validation here, the service layer 
        /// remains focused on business logic while ensuring data integrity is consistently enforced.
        /// </summary>
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
