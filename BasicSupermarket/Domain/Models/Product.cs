namespace BasicSupermarket.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public string Weight { get; set; } = "0";
    public string Description { get; set; } = String.Empty; 
    public int StockQuantity { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string ImageUrl { get; set; } = String.Empty;
    
    // Foreign Keys
    public int ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}