using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BasicSupermarket.Domain.Exceptions;

namespace BasicSupermarket.Domain.Entities;

public class CartItem: AuditableEntity
{
    private CartItem() { }
    
    #region Properties
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Cart")]
    public int CartId { get; set; }

    public Cart Cart { get; set; }

    [Required]
    public int ProductId { get; set; }
    
    public Product Product { get; set; } 

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    [NotMapped]
    public decimal Subtotal => Quantity * UnitPrice;
    
    #endregion
    
    #region Factory Methods

    public static CartItem Create(int cartId, int productId, int quantity)
    {
        if (cartId <= 0)
            throw new DomainException("CartId must be greater than zero");
        if (productId <= 0)
            throw new DomainException("ProductId must be greater than zero");
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero");

        var newCartItem = new CartItem
        {
            CartId = cartId,
            ProductId = productId,
            Quantity = quantity
        };
        return newCartItem;
    }
    #endregion
    
    #region Actions
    public void IncreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Amount must be greater than zero");
        Quantity += amount;
    }
    public void SetQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new DomainException("Quantity must be greater than zero");
        Quantity = newQuantity;
    }
    #endregion
}