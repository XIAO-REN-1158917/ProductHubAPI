using Demo.ASP.NET.Core.WebAPI.Server.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Demo.ASP.NET.Core.WebAPI.Server.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }

    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        [UniqueProductName]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string CategoryName { get; set; } = null!;
    }
}
