using Demo.ASP.NET.Core.WebAPI.Server.Data;
using Demo.ASP.NET.Core.WebAPI.Server.Models;
using Demo.ASP.NET.Core.WebAPI.Server.Services;
using Demo.ASP.NET.Core.WebAPI.Server.Repositories;
using Demo.ASP.NET.Core.WebAPI.Server.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services to the container
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Register ProductRepository
builder.Services.AddScoped<ProductService>();    // Register ProductService
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed data for development/demo environment
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        SeedData(context); // Call the seed function
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Method to seed initial data for demo purposes
static void SeedData(ApplicationDbContext context)
{
    if (!context.Categories.Any())
    {
        var electronics = new Category { Name = "Electronics" };
        var groceries = new Category { Name = "Groceries" };

        context.Categories.AddRange(electronics, groceries);

        context.Products.AddRange(
            new Product { Name = "Laptop", Category = electronics },
            new Product { Name = "Smartphone", Category = electronics },
            new Product { Name = "Apple", Category = groceries }
        );

        context.SaveChanges();
    }
}
