using OrderManagement.Application.Orders.Queries;
using OrderManagement.Domain;

namespace OrderManagement.Application.Orders;

[Mapper]
public static partial class OrderMapper
{
    public static partial OrderDto ToOrderDto(this Order order);
    public static partial OrderDetailDto ToOrderDetailDto(this Order order);
}