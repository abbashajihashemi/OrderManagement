using OrderManagement.Domain.Basic;

namespace OrderManagement.Domain;

public class Order : BaseEntity<Guid>
{
    public string Code { get; set; } = null!;
    public OrderStepStatus StepStatus { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TotalAmount { get; set; }
    public string? DiscountCode { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime CreatedAt { get; set; }

    #region Navigation Properties

    public List<OrderItem> Items { get; set; } = [];

    #endregion
}