using Demo.ASP.NET.Core.WebAPI.Server.Models;
using System.Linq.Expressions;

namespace Demo.ASP.NET.Core.WebAPI.Server.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<Product, bool>> predicate);
        Task<Category?> GetCategoryByNameAsync(string categoryName);
    }
}