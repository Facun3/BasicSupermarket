using BasicSupermarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Persistence;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet properties
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Product & Manufacturer relationship (Many-to-One)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Manufacturer)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.ManufacturerId);

        // Product & Category relationship (Many-to-One)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        // Order & User relationship (Many-to-One)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        // OrderItem & Product relationship (Many-to-One)
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        // OrderItem & Order relationship (Many-to-One)
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        // Order & Payment relationship (One-to-One)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Payment)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.PaymentId);
    }
}