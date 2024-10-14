using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Persistence;

namespace BasicSupermarket;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        // Seed Manufacturers
        if (!context.Manufacturers.Any())
        {
            context.Manufacturers.AddRange(
                new Manufacturer 
                { 
                    Name = "Fresh Goods Inc.", 
                    Country = "USA", 
                    Phone = "+1-800-1234", 
                    Email = "info@freshgoods.com", 
                    WebsiteUrl = "https://www.freshgoods.com", 
                    Address = "123 Fresh St, New York, USA" 
                },
                new Manufacturer 
                { 
                    Name = "Organic Farms Co.", 
                    Country = "Canada", 
                    Phone = "+1-800-5678", 
                    Email = "contact@organicfarms.com", 
                    WebsiteUrl = "https://www.organicfarms.com", 
                    Address = "456 Green Lane, Ontario, Canada" 
                },
                new Manufacturer 
                { 
                    Name = "Sweet Treats Ltd.", 
                    Country = "UK", 
                    Phone = "+44-20-7890", 
                    Email = "support@sweettreats.co.uk", 
                    WebsiteUrl = "https://www.sweettreats.co.uk", 
                    Address = "789 Candy Ave, London, UK" 
                },
                new Manufacturer 
                { 
                    Name = "Dairy Best", 
                    Country = "Netherlands", 
                    Phone = "+31-20-12345", 
                    Email = "sales@dairybest.nl", 
                    WebsiteUrl = "https://www.dairybest.nl", 
                    Address = "101 Dairy St, Amsterdam, Netherlands" 
                },
                new Manufacturer 
                { 
                    Name = "Global Beverages Corp.", 
                    Country = "Germany", 
                    Phone = "+49-30-54321", 
                    Email = "info@globalbev.de", 
                    WebsiteUrl = "https://www.globalbev.de", 
                    Address = "202 Beverage Blvd, Berlin, Germany" 
                }
            );
        }
        
        // Seed Categories
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Fruits", Description = "Fresh and organic fruits." },
                new Category { Name = "Bakery", Description = "Freshly baked bread and pastries." },
                new Category { Name = "Dairy", Description = "Milk, cheese, yogurt, and more." },
                new Category { Name = "Beverages", Description = "Juices, sodas, and water." },
                new Category { Name = "Grocery", Description = "Pantry staples and snacks." }
            );
        }

        // Seed Products
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product 
                { 
                    Name = "Organic Banana", 
                    Price = 0.99m, 
                    Weight = "150g", 
                    Description = "Fresh organic bananas", 
                    StockQuantity = 100, 
                    ExpiryDate = DateTime.Now.AddDays(5), 
                    ImageUrl = "https://example.com/banana.jpg", 
                },
                new Product 
                { 
                    Name = "Whole Wheat Bread", 
                    Price = 2.49m, 
                    Weight = "500g", 
                    Description = "Whole wheat bread loaf", 
                    StockQuantity = 50, 
                    ExpiryDate = DateTime.Now.AddDays(7), 
                    ImageUrl = "https://example.com/bread.jpg"
                },
                new Product 
                { 
                    Name = "Chocolate Chip Cookies", 
                    Price = 3.99m, 
                    Weight = "250g", 
                    Description = "Crispy cookies with chocolate chips", 
                    StockQuantity = 75, 
                    ExpiryDate = DateTime.Now.AddMonths(2), 
                    ImageUrl = "https://example.com/cookies.jpg"
                },
                new Product 
                { 
                    Name = "Fresh Orange Juice", 
                    Price = 5.99m, 
                    Weight = "1000ml", 
                    Description = "100% pure orange juice", 
                    StockQuantity = 120, 
                    ExpiryDate = DateTime.Now.AddDays(10), 
                    ImageUrl = "https://example.com/juice.jpg"
                },
                new Product 
                { 
                    Name = "Cheddar Cheese", 
                    Price = 4.99m, 
                    Weight = "200g", 
                    Description = "Mature cheddar cheese", 
                    StockQuantity = 40, 
                    ExpiryDate = DateTime.Now.AddMonths(3), 
                    ImageUrl = "https://example.com/cheddar.jpg"
                }
            );
        }
        
        context.SaveChanges();
    }
}