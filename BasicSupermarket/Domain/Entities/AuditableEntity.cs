namespace BasicSupermarket.Domain.Entities;

public abstract class AuditableEntity
{
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastModifyDate { get; set; }
}