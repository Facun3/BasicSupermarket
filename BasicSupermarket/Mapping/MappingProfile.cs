using AutoMapper;
using BasicSupermarket.Domain.Dto.Cart;
using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        // Order Mappings
        // CreateMap<Order, OrderResponseDto>();
        // CreateMap<OrderItem, OrderItemResponseDto>();
        // CreateMap<CreateOrderRequestDto, Order>();
        //
        // Payment Mappings
        // CreateMap<Payment, PaymentResponseDto>();

        // Cart Mappings
        CreateMap<Cart, CartResponseDto>();
        CreateMap<CartItem, CartItemResponseDto>();
        CreateMap<AddCartItemRequestDto, CartItem>();
    }
    
}