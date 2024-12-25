using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Url]
    public string ImageUrl { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Product: {Name}, Description: {Description}";
    }
}
