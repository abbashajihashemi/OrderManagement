using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using OrderManagement.Application.Inventory;

namespace OrderManagement.Infrastructure.Clients.Inventory;

public class InventoryClient(
    HttpClient httpClient,
    ILogger<InventoryClient> logger
) : IInventoryClient
{
    public async Task<IReadOnlyList<ProductStockResponse>> GetStockAsync(IEnumerable<long> productCodes, CancellationToken ct = default)
    {
        var ids = productCodes.Distinct().ToArray();
        if (ids.Length == 0)
        {
            logger.LogWarning("Stock inquiry with invalid empty productCodes");
            return [];
        }

        var body = new StockQueryRequest(ids);
        logger.LogInformation("Stock inquiry sending request {body}", body);

        using var response = await httpClient.PostAsJsonAsync(
            "api/v1/inventory/inquiry", body, ct);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ProductStockResponse>>(ct) ?? [];
    }

    private sealed record StockQueryRequest(IReadOnlyCollection<long> ProductCodes);
}