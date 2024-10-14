namespace BasicSupermarket.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public string PaymentMethod { get; set; } = String.Empty;
    public string PaymentStatus { get; set; } = null!;
    public DateTime PaymentDate { get; set; }
    public string TransactionId { get; set; } = null!;

    // Navigation Property
    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}