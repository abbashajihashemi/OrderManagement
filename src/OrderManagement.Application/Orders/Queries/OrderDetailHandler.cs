using Microsoft.Extensions.Logging;
using OrderManagement.Application.Abstractions.Persistence;
using OrderManagement.Application.Inventory;
using OrderManagement.Domain;

namespace OrderManagement.Application.Orders.Queries;

public class OrderDetailHandler(
    IOrderRepository orderRepository,
    IInventoryClient inventoryClient,
    ILogger<OrderDetailHandler> logger
) : IHandler<OrderDetailQuery, Result<OrderDetailDto>>
{
    public async Task<Result<OrderDetailDto>> HandleAsync(OrderDetailQuery request, CancellationToken ct = default)
    {
        logger.LogInformation("OrderDetailHandler start for order by id {id}", request.Id);

        var order = await orderRepository.GetOrderWithItemsByIdAsync(request.Id, ct);
        if (order == null)
        {
            logger.LogWarning("OrderDetailHandler order not found {id}", request.Id);
            return Result<OrderDetailDto>.Failure("Order not found");
        }

        var itemsInventories = await GetProductsInventories(order, ct);
        var result = MapOrderToResult(order, itemsInventories);

        logger.LogInformation("OrderDetailHandler finish for order by id {id}", request.Id);
        return Result<OrderDetailDto>.Success(result);
    }

    private async ValueTask<IReadOnlyList<ProductStockResponse>> GetProductsInventories(
        Order order,
        CancellationToken ct = default)
    {
        if (order.StepStatus != OrderStepStatus.Unpaid)
        {
            logger.LogInformation("OrderDetailHandler don't need get inventory. Step status {stepStatus}", order.StepStatus);
            return [];
        }

        var productCodes = order.Items.Select(x => x.ProductCode).ToList();
        return await inventoryClient.GetStockAsync(productCodes, ct);
    }

    private static OrderDetailDto MapOrderToResult(
        Order order,
        IReadOnlyList<ProductStockResponse> itemsInventories)
    {
        var result = order.ToOrderDetailDto();

        foreach (var item in result.Items)
        {
            var itemInventory = itemsInventories.SingleOrDefault(x => x.ProductCode == item.ProductCode);
            item.AvailableQuantity = itemInventory?.AvailableQuantity ?? 0;
        }

        return result;
    }
}
























