namespace BasicSupermarket.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = String.Empty;

    // Navigation Property
    public ICollection<Product> Products { get; set; } = new List<Product>();
}