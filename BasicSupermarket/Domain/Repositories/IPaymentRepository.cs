using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Domain.Repositories;

public interface IPaymentRepository: IRepository<Payment>
{
    Task<Payment?> GetPaymentByOrderIdAsync(int orderId);
}