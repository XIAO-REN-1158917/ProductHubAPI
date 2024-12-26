# Demo_ASP.NET-Core-Web-API



Thank you for taking the time to review my project. This demo showcases a modular ASP.NET Core Web API project designed to demonstrate my understanding of backend development, layered architecture, and teamwork.

The primary objective is to provide a clear structure, making it maintainable, extensible, and easy to understand. While this is a demo project, the approach used can be scaled or simplified depending on the requirements of a real-world application.



## 1. Objective

This project focuses on **structure and modular development** rather than extensive functionality. It is designed to highlight:

- My ability to create clean, modular codebases.
- My understanding of teamwork by designing with future collaboration in mind.
- My proficiency in implementing unit tests for core business logic.

The structure is intentionally layered to reflect industry best practices. This ensures that the codebase is maintainable, easy to extend, and adaptable to different team workflows.



## 2. Project Structure

### Overview

The project follows a **layered architecture**. Below is an overview of the main components:

### 

#### Attributes

Stores custom attributes, such as `UniqueProductNameAttribute`, used to validate specific constraints like the uniqueness of product names.

#### Common

Contains utility classes, such as `ApiResponse`, which standardise and encapsulate API responses for consistency.

#### Controller

Contains RESTful API controllers that handle HTTP requests and delegate logic to the service layer.

-For example, `ProductController` provides CRUD actions for managing products.

#### Data

Contains the `ApplicationDbContext` class, which manages database connections and entity mappings for EF Core.

#### DTOs (Data Transfer Objects)

Decouples models from the data exposed through APIs.

-For example, `ProductDto` encapsulates the input(create) and output(response) data for products.

#### Middlewares

Includes custom middleware components to handle cross-cutting concerns.

-Example: `ExceptionHandlingMiddleware` centralises exception handling by logging errors and returning consistent error responses.

#### Migrations

Manages database schema versioning with EF Core migrations, enabling easy updates and rollback.

#### Models

Defines database models(entities) that are mapped to database tables.

#### Repositories

Data Access Layer

- Example: `ProductRepository` implements `IProductRepository`, which allows seamless substitution with a mocked repository for testing.
- **Note**: The use of interfaces highlights my understanding of **programming to an interface**, enabling flexibility and testability.

#### Services

Implements the **Business Logic Layer**, encapsulating business rules and coordinating interactions with the repository layer.

#### Test

Contains unit tests for the service layer, demonstrating my understanding of testing core business logic in isolation.

**Key Features**:

- Focused on testing the service layer (business logic).


- Uses `Mock<IProductRepository>` to isolate service logic from the database.


- Key testing packages

```bash
dotnet add package xunit
dotnet add package Moq
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit.runner.visualstudio
```

- To execute the text

```bash
dotnet test
```

- **Note**: This demonstrates my understanding of its importance and ability to implement basic unit tests.

#### Validation

Contains reusable validation utilities, such as `DtoValidator`, ensuring data integrity at the DTO level.

#### appsettings.json & appsettings.Development.json

Stores configuration settings for different environments (e.g., database connection strings).

#### Program.cs

entry point

- Registering services (e.g., repositories, business logic).
- Configuring middleware (e.g., exception handling).
- Setting up the API pipeline.

**Tip**: Use Swagger to test the APIs after running the application.

### **3. Notable Features**

- **Layered Architecture**: Clean separation of concerns into controllers, services, repositories, and data layers.
- **Testability**: Interface-based repository implementation allows easy testing of the service layer using Mock objects.
- **Modular Design**: Each module is self-contained, with a single responsibility, ensuring maintainability.
- **Error Handling**: Centralised exception handling through middleware.
- **Configuration**: Environment-specific settings handled through `appsettings.json`.

### **4.  Final Notes**

This project serves as an introduction to my backend development skills. It highlights my understanding of modular programming, interface-based design, and unit testing. While it is not a production-ready application, it reflects my ability to adapt to different team workflows and maintain code quality.

Thank you for your time! I look forward to discussing this project and how I can contribute to your team.