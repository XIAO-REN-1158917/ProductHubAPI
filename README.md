# Demo_ASP.NET-Core-Web-API



**First of all, thank you very much for taking the time to review my CV.**



## 1. Objective

This is a demo of an ASP.NET Core Web API project, designed to help you understand my approach to Web API development. The demo focuses on structure rather than functionality. 

Through this modular approach, I aim to demonstrate my ability to collaborate effectively with your team. Of course, the structure can be simplified and consolidated or further divided and refined based on the specific requirements of a real project.



## 2. Introduction to each module

### Overview

This project follows a layered architecture to showcase modular development and teamwork capabilities. The project is divided into multiple independent modules, each with a single and clear responsibility, making it easy to maintain and extend.

### Introduction

#### Attributes

Store custom attributes, such as `UniqueProductNameAttribute`, which is used to validate the uniqueness of product names.

#### Common

Includes utility classes, such as `ApiResponse`, used to encapsulate a unified response format for APIs.

#### Controller

Contains RESTful API controllers to handle HTTP requests and invoke the service layer for business logic execution.

-For example, `ProductController` provides CRUD actions for products.

#### Data

Contains the database context class, which manages the connection to the database and entity mappings.

#### DTOs

Defines Data Transfer Objects (DTOs) to decouple models from API request/response data.

-For example, `ProductDto` encapsulates the input(create) and output(response) data for products.

#### Middlewares

Contains custom middleware to intercept HTTP requests and perform specific actions.

-`ExceptionHandlingMiddleware` captures exceptions, logs the details, and returns standardised error responses.

#### Migrations

Database migration folder, used to manage version control for the database schema.

#### Models

Defines database models(entities) that are mapped to database tables.

#### Repositories

Data Access Layer, encapsulates the logic for direct interactions with the database.

#### Services

Business Logic Layer, encapsulates business rules and interacts with the repository Layer.

#### Test

```bash
dotnet add package xunit
dotnet add package Moq
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit.runner.visualstudio
```



#### Validation

A dedicated repository for data validation tools, where commonly used validation utilities can be encapsulated and reused.

#### appsettings.json & appsettings.Development.json

Stores project configurations, such as database connection strings and other environment settings.

#### Program.cs

Project entry file, responsible for configuring services, setting up the middleware pipeline, and running the Web API.



**Tip: After starting the application, you can use Swagger to test the APIs.**