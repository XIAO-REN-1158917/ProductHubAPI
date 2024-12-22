using System.ComponentModel.DataAnnotations;

namespace Demo.ASP.NET.Core.WebAPI.Server.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
