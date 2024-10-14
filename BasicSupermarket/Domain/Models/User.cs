namespace BasicSupermarket.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Phone { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string PostalCode { get; set; } = String.Empty;

    // Navigation Property
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}