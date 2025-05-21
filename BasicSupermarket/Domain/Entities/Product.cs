using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BasicSupermarket.Domain.Exceptions;

namespace BasicSupermarket.Domain.Entities;

public class Product: AuditableEntity
{
    private Product() {}
    
    #region Properties
    [Key]
    public int Id { get; private set; }

    [Required, MinLength(3), MaxLength(100)]
    public string Name { get; private set; } = string.Empty;
    
    [Required, MinLength(10), MaxLength(500)]
    public string Description { get; private set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; private set; }

    [Required] [Range(0, int.MaxValue)] public int Quantity { get; private set; } = 0;
    
    [Url]
    public string ImageUrl { get; private set; } = string.Empty;
    
    public int CategoryId { get; private set; }
    
    public Category Category { get; private set; } = default!;
    #endregion
    
    #region Factory methods
    public static Product Create(string name, string description, decimal price, int quantity, int categoryId)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 100)
            throw new DomainException("Name must be between 3 and 100 characters");
        if (string.IsNullOrWhiteSpace(description) || description.Length < 10 || description.Length > 500)
            throw new DomainException("Description must be between 10 and 500 characters");
        if (price < 0) 
            throw new DomainException("Price must be greater than or equal to zero");
        if (quantity < 0) 
            throw new DomainException("Quantity must be greater than or equal to zero");
        if (categoryId < 0)
            throw new DomainException("CategoryId must be greater than or equal to zero");
        
        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            Quantity = quantity,
            CategoryId = categoryId
        };
        
        return product;
    }
    
    #endregion
    
    #region Actions
    
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0) 
            throw new DomainException("Price must be greater than zero");
        Price = newPrice;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity < 0) 
            throw new DomainException("Quantity must be greater than zero");
        Quantity = newQuantity;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0) 
            throw new DomainException("Quantity must be greater than zero");
        Quantity += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0) 
            throw new DomainException("Quantity must be greater than zero");
        if (Quantity < quantity) 
            throw new DomainException("Insufficient stock to remove");
        Quantity -= quantity;
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription)) 
            throw new DomainException("Description cannot be empty");
        Description = newDescription;
    }
    
    public void UpdateCategory(int newCategoryId)
    {
        if (newCategoryId < 0) 
            throw new DomainException("Category must be zero or greater than zero");
        CategoryId = newCategoryId;
    }

    public void UpdateImageUrl(string imageUrl)
    {
        if (!string.IsNullOrEmpty(imageUrl) && !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            throw new DomainException("Image URL is invalid");
        ImageUrl = imageUrl;
    }
    
    public void UpdateDetails(string name, string description, decimal price, string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 100)
            throw new DomainException("Name must be between 3 and 100 characters");
        if (string.IsNullOrWhiteSpace(description) || description.Length < 10 || description.Length > 500)
            throw new DomainException("Description must be between 10 and 500 characters");
        if (price <= 0)
            throw new DomainException("Price must be greater than zero");
        if (!string.IsNullOrEmpty(imageUrl) && !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            throw new DomainException("Image URL is invalid");

        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }
    
    #endregion
    
    // Internal method, for testing purposes
    internal void SetCategory(Category category)
    {
        Category = category;
        CategoryId = category?.Id ?? 0;
    }

    internal void SetId(int id)
    {
        Id = id;
    }
    
    public override string ToString()
    {
        return $"Product: {Name}, Description: {Description}";
    }
}
