using System.ComponentModel.DataAnnotations;
using BasicSupermarket.Domain.Exceptions;

namespace BasicSupermarket.Domain.Entities;

public class Cart: AuditableEntity
{
    private Cart () { }
    
    #region Properties
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; private set; }

    private readonly List<CartItem> _items  = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    
    public decimal TotalPrice => _items.Sum(i => i.Subtotal);
    #endregion
    #region Factory Methods

    public static Cart Create(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId)) throw new DomainException(nameof(userId));
        return new Cart{UserId = userId};
    }
    #endregion
    #region Actions

    public void AddItem(CartItem item)
    {
        if (item == null)
            throw new DomainException("CartItem cannot be null");

        var existingItem = _items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(item.Quantity);
        }
        else
        {
            _items.Add(item);
        }
    }


    public void RemoveItem(int productId)
    {
        var itemToRemove = _items.FirstOrDefault(i => i.ProductId == productId);
        if (itemToRemove == null)
            throw new DomainException("Item not found in cart");
        _items.Remove(itemToRemove);
    }

    public void UpdateItemQuantity(int productId, int newQuantity)
    {
        if(newQuantity <= 0)
            throw new DomainException("Quantity must be greater than zero");
        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
        if(existingItem == null)
            throw new DomainException("Item not found in cart");
        existingItem.SetQuantity(newQuantity);
    }

    public void Clear()
    {
        _items.Clear();
    }
    #endregion
}