
using BasicSupermarket.Domain.Dto.Order;
using BasicSupermarket.Domain.Services.Communication;

namespace BasicSupermarket.Domain.Services;

public interface IOrderService
{
    Task<Response<OrderResponseDto>> CreateOrderAsync(string userId);
    
}