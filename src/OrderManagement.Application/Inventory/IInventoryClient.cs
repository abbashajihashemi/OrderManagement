namespace OrderManagement.Application.Inventory;

public interface IInventoryClient
{
    Task<IReadOnlyList<ProductStockResponse>> GetStockAsync(IEnumerable<long> productCodes, CancellationToken ct = default);
}