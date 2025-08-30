# BasicSupermarket API

A .NET Core Web API project that implements a basic supermarket management system with features for product management, shopping cart functionality, and user authentication.

## Technologies Used

- .NET 8.0
- Entity Framework Core
- ASP.NET Core Identity
- AutoMapper
- xUnit for Testing
- Docker support

## Features

- Product Management
  - Create, Read, Update, Delete (CRUD) operations
  - Product categorization
  - Product filtering and pagination

- Shopping Cart
  - Add/Remove items
  - View cart contents
  - Clear cart

- Category Management
  - CRUD operations for product categories

- User Authentication
  - JWT-based authentication
  - User registration and login

- Error Handling
  - Global error handling middleware
  - Standardized error responses

## Project Structure

- `BasicSupermarket/`
  - `Controllers/` - API endpoints implementation
  - `Domain/` - Business logic and entities
  - `Services/` - Business logic implementation
  - `Persistence/` - Database context and migrations
  - `Dtos/` - Data Transfer Objects
  - `Mapping/` - AutoMapper profiles

- `BasicSupermarketTests/`
  - Unit tests for controllers and services
  - Integration tests

## Getting Started

1. Clone the repository
2. Ensure you have .NET 8.0 SDK installed
3. Install dependencies:
```bash
dotnet restore
```

4. Database Setup:
```bash
# Create a new migration
dotnet ef migrations add InitialCreate --project BasicSupermarket

# Apply migrations to the database
dotnet ef database update --project BasicSupermarket
```

5. Run the application:
```bash
dotnet run --project BasicSupermarket
```

6. For Docker deployment:
```bash
docker-compose up
```

## Testing

Run the tests using:
```bash
dotnet test
```

## API Documentation

The API includes endpoints for:

- Authentication
  - POST /api/auth/login
  - POST /api/auth/register

- Products
  - GET /api/products
  - GET /api/products/{id}
  - POST /api/products
  - PUT /api/products/{id}
  - DELETE /api/products/{id}

- Categories
  - GET /api/categories
  - POST /api/categories

- Shopping Cart
  - GET /api/cart
  - POST /api/cart/add
  - POST /api/cart/remove
  - POST /api/cart/clear

## Contributing

1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch
5. Create a new Pull Request
