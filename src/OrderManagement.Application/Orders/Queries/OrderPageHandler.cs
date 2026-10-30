using Microsoft.Extensions.Logging;
using OrderManagement.Application.Abstractions.Persistence;

namespace OrderManagement.Application.Orders.Queries;

public class OrderPageHandler(
    IOrderRepository orderRepository,
    ILogger<OrderPageHandler> logger
) : IHandler<OrderPageQuery, Result<PagedResult<OrderDto>>>
{
    public async Task<Result<PagedResult<OrderDto>>> HandleAsync(OrderPageQuery request, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(request.Filter);

        var data = await orderRepository.SearchAsync(request.Filter, ct);
        logger.LogInformation("OrderPageQuery data fetched successfully, count {count}", data.Items.Count());

        var mapData = data.Map(x => x.ToOrderDto());
        logger.LogInformation("OrderPageQuery mapped successfully");

        return Result<PagedResult<OrderDto>>.Success(mapData);
    }
}