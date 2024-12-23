using Microsoft.EntityFrameworkCore;
using Demo.ASP.NET.Core.WebAPI.Server.Models;

namespace Demo.ASP.NET.Core.WebAPI.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //In my demo, I used EF Core's convention-based configuration
        //because it is faster and suitable for simple scenarios, such as one-to-one mapping between models and database tables.
        //With conventions, EF Core automatically identifies relationships like foreign keys and primary keys,
        //making development more convenient.
        //
        //However, I also understand that EF Core supports explicit configuration using Fluent API,
        //which is better suited for complex scenarios,
        //such as custom foreign key relationships, indexes, or constraints.
        //In real-world projects, when dealing with complex database requirements,
        //I would use the OnModelCreating method for explicit configuration. 

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);


        //    modelBuilder.Entity<Category>()
        //        .HasMany(c => c.Products)
        //        .WithOne(p => p.Category)
        //        .HasForeignKey(p => p.CategoryId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
