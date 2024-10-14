namespace BasicSupermarket.Domain.Entities;

public class Manufacturer
{
    public int ManufacturerId { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string WebsiteUrl { get; set; } = String.Empty; 
    public string Address { get; set; } = String.Empty; 
    public string Country { get; set; } = String.Empty;

    // Navigation Property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}