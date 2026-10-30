using System.Collections.Concurrent;
using OrderManagement.Application.Orders.Queries;
using OrderManagement.Domain;

namespace OrderManagement.Infrastructure.Persistence;

public class InMemoryOrderRepository : IOrderRepository
{
    private static readonly ConcurrentDictionary<Guid, Order> Store = new();

    public InMemoryOrderRepository()
    {
        Seed();
    }

    public Task<PagedResult<Order>> SearchAsync(OrderPageFilter filter, CancellationToken ct = default)
    {
        IEnumerable<Order> storeQuery = Store.Values;

        if (filter.MinTotal is { } min)
            storeQuery = storeQuery.Where(x => x.TotalAmount >= min);

        if (filter.MaxTotal is { } max)
            storeQuery = storeQuery.Where(x => x.TotalAmount <= max);

        var result = storeQuery
            .OrderByDescending(x => x.CreatedAt)
            .ToPagedResult(filter);

        return Task.FromResult(result);
    }

    public Task<Order?> GetOrderWithItemsByIdAsync(Guid id, CancellationToken ct = default)
    {
        Store.TryGetValue(id, out var order);
        return Task.FromResult(order);
    }

    private static void Seed()
    {
        var statuses = Enum.GetValues<OrderStepStatus>();

        var products = new (long Code, string Title, decimal Price)[]
        {
            (1001, "Wireless Mouse", 25.90m),
            (1002, "Mechanical Keyboard", 89.00m),
            (1003, "USB-C Hub", 39.50m),
            (1004, "27\" Monitor", 249.99m),
            (1005, "Laptop Stand", 34.00m),
            (1006, "Webcam HD", 59.90m),
        };

        // A few repeat customers so several orders share the same CustomerId
        var customers = Enumerable.Range(1, 4)
            .Select(n => new Guid(n, 0, 0, new byte[8]))
            .ToArray();

        // (code, percent)
        var discounts = new (string Code, decimal Percent)[]
        {
            ("WELCOME10", 0.10m),
            ("SPRING15", 0.15m),
            ("VIP20", 0.20m)
        };

        var now = DateTime.UtcNow;

        for (var i = 1; i <= 12; i++)
        {
            var orderId = OrderSeedIds.All[i - 1];

            var order = new Order
            {
                Id = orderId,
                Code = $"ORD-{now:yyyyMMdd}-{i:D4}",
                StepStatus = statuses[(i - 1) % statuses.Length],
                CustomerId = customers[(i - 1) % customers.Length],
                CreatedAt = now.AddDays(-i).AddMinutes(-i * 7),
            };

            var itemCount = (i % 4) + 1; // 1..4 items
            for (var j = 0; j < itemCount; j++)
            {
                var p = products[(i + j) % products.Length];
                order.Items.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductCode = p.Code,
                    ProductTitle = p.Title,
                    Quantity = ((i + j) % 3) + 1,
                    UnitPrice = p.Price,
                    Order = order
                });
            }

            order.Subtotal = order.Items.Sum(x => x.UnitPrice * x.Quantity);

            // Roughly 2 of every 3 orders get a discount; the rest have none
            if (i % 3 != 0)
            {
                var d = discounts[i % discounts.Length];
                order.DiscountCode = d.Code;
                order.DiscountAmount = Math.Round(order.Subtotal * d.Percent, 2);
            }
            else
            {
                order.DiscountCode = null;
                order.DiscountAmount = 0m;
            }

            order.TotalAmount = order.Subtotal - order.DiscountAmount;
            Store[order.Id] = order;
        }
    }
}