using Demo.ASP.NET.Core.WebAPI.Server.Data;
using Demo.ASP.NET.Core.WebAPI.Server.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.ASP.NET.Core.WebAPI.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all products
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        // Get product by ID
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        // Add new product
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Update info of product
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // Delete product
        //public async Task DeleteAsync(int id)
        //{
        //    var product = await GetByIdAsync(id);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        // Delete product
        public async Task DeleteAsync(Product product)
        {
           _context.Products.Remove(product);
           await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if any product exists that matches the given condition.
        /// </summary>
        /// <param name="predicate">The condition to evaluate (e.g., p => p.Name == "Apple").</param>
        /// <returns>True if a product exists; otherwise, false.</returns>
        public async Task<bool> ExistsAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.AnyAsync(predicate);
        }

        // Get a category by name
        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        }

        //public async Task CreateEntityViaStoreProcedure()
        //{
        //    //var columnName = "Url";
        //    //var columnValue = new SqlParameter("columnValue", "http://SomeURL");

        //    //var blogs = await context.Blogs
        //    //    .FromSqlRaw($"SELECT * FROM [Blogs] WHERE {columnName} = @columnValue", columnValue)
        //    //    .ToListAsync();
        //}

    }
}
