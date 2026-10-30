using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using OrderManagement.Application.Inventory;

namespace OrderManagement.Infrastructure.Clients.Inventory;

public sealed class InMemoryInventoryClient(
    ILogger<InMemoryInventoryClient> logger
) : IInventoryClient
{
    private static readonly ConcurrentDictionary<long, int> Stock = new()
    {
        [1001] = 100, // Wireless Mouse: plenty
        [1002] = 15,  // Mechanical Keyboard: moderate
        [1003] = 3,   // USB-C Hub: low, fails when quantity > 3
        [1004] = 0,   // 27" Monitor: out of stock
        [1005] = 50,  // Laptop Stand
        [1006] = 1,   // Webcam HD: only one left
    };

    public Task<IReadOnlyList<ProductStockResponse>> GetStockAsync(
        IEnumerable<long> productCodes, CancellationToken ct = default)
    {
        var ids = productCodes.Distinct().ToArray();
        if (ids.Length == 0)
            return Task.FromResult<IReadOnlyList<ProductStockResponse>>([]);

        var result = ids
            .Where(Stock.ContainsKey)
            .Select(code => new ProductStockResponse
            {
                ProductCode = code,
                AvailableQuantity = Stock[code]
            })
            .ToList();

        return Task.FromResult<IReadOnlyList<ProductStockResponse>>(result);
    }
}