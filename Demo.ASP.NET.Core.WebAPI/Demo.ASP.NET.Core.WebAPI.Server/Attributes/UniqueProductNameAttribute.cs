using System.ComponentModel.DataAnnotations;
using Demo.ASP.NET.Core.WebAPI.Server.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.ASP.NET.Core.WebAPI.Server.Attributes
{
    public class UniqueProductNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Retrieve ProductService from the dependency injection container.
            //var productService = (ProductService)validationContext.GetService(typeof(ProductService));
            var productService = validationContext.GetService(typeof(ProductService)) as ProductService;
            if (productService == null)
            {
                return new ValidationResult("Validation service is not available.");
            }

            var name = value as string;

            if (productService == null || string.IsNullOrEmpty(name))
            {
                return new ValidationResult("Validation service or value is missing.");
            }

            // Check if the product name already exists
            var existsTask = productService.IsProductNameTakenAsync(name);
            existsTask.Wait(); // Avoid asynchronous calls
            if (existsTask.Result)
            {
                return new ValidationResult("Product name already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
