using Demo.ASP.NET.Core.WebAPI.Server.Data;
using Demo.ASP.NET.Core.WebAPI.Server.Models;
using Demo.ASP.NET.Core.WebAPI.Server.Services;
using Demo.ASP.NET.Core.WebAPI.Server.Repositories;
using Demo.ASP.NET.Core.WebAPI.Server.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    //The default policy is used here for convenience in testing,
    //but a named policy can also be used to specify the trusted party.

    // Example of named policy
    //options.AddPolicy("TrustedName",
    //    builder => {
    //        builder.WithOrigins("https://something");
    //    });
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader();
            builder.WithExposedHeaders(new string[] { "isexpired" });
        });
});

builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(j =>
{
    j.RequireHttpsMetadata = false;
    j.SaveToken = true;
    j.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey123456789012345678901234567890")),
        ValidateIssuer = true,
        ValidIssuer = "Gary",
        ValidateAudience = true,
        ValidAudience = "TokenDemoAPI",
        ValidateLifetime = true,
        //This disables the time skew, which is useful for tokens with very short lifetimes and can be used during testing.
        //ClockSkew = TimeSpan.Zero 
    };
    j.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Append("isexpired", "true");
            }
            return Task.CompletedTask;
        },

        // Custom forbidden event for insufficient permissions
        OnForbidden = async context =>
        {
            var requestedEndpoint = context.HttpContext.Request.Path;
            string responseMessage;

            // Customise the message based on the endpoint
            if (requestedEndpoint.StartsWithSegments("/api/TokenDemo/staff"))
            {
                responseMessage = "No Staff Permission";
            }
            else if (requestedEndpoint.StartsWithSegments("/api/TokenDemo/admin"))
            {
                responseMessage = "No Admin Permission";
            }
            else
            {
                responseMessage = "Access Denied";
            }

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync($"{{\"message\": \"{responseMessage}\"}}");
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StaffLevel", policy => 
        policy.RequireRole("staff").Build());// Only users with "Staff" role can access
    options.AddPolicy("AdminLevel", policy =>
        policy.RequireRole("admin")); // Only users with "Admin" role can access
});




// Configure the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register services to the container

//DI -> level - transi
//builder.Services.AddTransient - 
//builder.Services.AddScopeds
//builder.Services.AddSingleton

builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Register ProductRepository
builder.Services.AddScoped<IProductService, ProductService>();    // Register ProductService
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
//This allows the service to be started using an exe file to run in the background,
//making it convenient to conduct tests on a single device.
if (app.Environment.IsProduction())
{
    app.Urls.Add("https://*:6002");
}

// Middleware pipeline
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(); // Enable CORS
// Example of named policy
//app.UseCors("TrustedName")

app.UseAuthentication();

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
//static void SeedData(ApplicationDbContext context)
//{
//    context.Database.EnsureDeleted();
//    context.Database.EnsureCreated();

//    var electronics = new Category { Name = "Electronics" };
//    var groceries = new Category { Name = "Groceries" };

//    context.Categories.AddRange(electronics, groceries);

//    context.Products.AddRange(
//        new Product { Name = "Laptop", Category = electronics },
//        new Product { Name = "Smartphone", Category = electronics },
//        new Product { Name = "Apple", Category = groceries }
//    );

//    context.SaveChanges();
//}
