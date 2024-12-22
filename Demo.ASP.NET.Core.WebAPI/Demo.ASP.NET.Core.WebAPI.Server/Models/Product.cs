using System.ComponentModel.DataAnnotations;

namespace Demo.ASP.NET.Core.WebAPI.Server.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
