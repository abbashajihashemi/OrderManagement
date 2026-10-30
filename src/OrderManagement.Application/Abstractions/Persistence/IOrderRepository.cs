using OrderManagement.Application.Orders.Queries;
using OrderManagement.Domain;

namespace OrderManagement.Application.Abstractions.Persistence;

public interface IOrderRepository
{
    Task<PagedResult<Order>> SearchAsync(OrderPageFilter filter, CancellationToken ct = default);
    Task<Order?> GetOrderWithItemsByIdAsync(Guid id, CancellationToken ct = default);
}