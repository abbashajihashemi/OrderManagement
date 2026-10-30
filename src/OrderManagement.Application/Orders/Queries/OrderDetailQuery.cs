using OrderManagement.Domain;

namespace OrderManagement.Application.Orders.Queries;

public sealed record OrderDetailQuery(Guid Id) : IRequest<Result<OrderDetailDto>>;

public class OrderDetailDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public OrderStepStatus StepStatus { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<OrderDetailItemDto> Items { get; set; } = [];
}

public class OrderDetailItemDto
{
    public long ProductCode { get; set; }
    public string ProductTitle { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int? AvailableQuantity { get; set; }
}