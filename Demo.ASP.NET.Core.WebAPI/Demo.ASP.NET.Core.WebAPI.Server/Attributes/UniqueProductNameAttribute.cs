using System.ComponentModel.DataAnnotations;
using Demo.ASP.NET.Core.WebAPI.Server.Services;

namespace Demo.ASP.NET.Core.WebAPI.Server.Attributes
{
    public class UniqueProductNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Retrieve ProductService from the dependency injection container.
            var productService = validationContext.GetRequiredService<IProductService>();

            var name = (string)value!;
           
            // Check if the product name already exists
            var existsTask = productService.IsProductNameTakenAsync(name);
            existsTask.Wait(); // Avoid asynchronous calls
            //In the Controller, I provided asynchronous methods to support real-time validation for the front-end,
            //while custom attributes are primarily used for back-end form validation.

            //Considering that ASP.NET Core's default validation mechanism is synchronous,
            //I chose to use synchronous implementation in the attributes to keep the code more concise.

            // For high-performance requirements or complex business scenarios,
            // I would prioritise asynchronous validation.
            if (existsTask.Result)
            {
                return new ValidationResult("Product name already exists.");
            }

            return ValidationResult.Success;
        }
    }
}

//In this backend validation scenario, I rely on ASP.NET Core's built-in model validation mechanism to handle ValidationResult.
//The framework automatically stores ValidationResult(string) in ModelState and returns an HTTP 400 response with error details when validation fails.
//This approach reduces the amount of code needed for explicitly handling validation results and improves development efficiency.