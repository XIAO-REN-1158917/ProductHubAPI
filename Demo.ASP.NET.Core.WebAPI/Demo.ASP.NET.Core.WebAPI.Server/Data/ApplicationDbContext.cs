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

        //It is also possible to use this method for explicit configuration to handle more complex logic.
        //In this demo, I use EF Core's automatic inference mechanism.

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
