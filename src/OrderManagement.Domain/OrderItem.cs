using OrderManagement.Domain.Basic;

namespace OrderManagement.Domain;

public class OrderItem : BaseEntity<Guid>
{
    public Guid OrderId { get; set; }
    public long ProductCode { get; set; }
    public string ProductTitle { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    #region Navigation Properties

    public Order? Order { get; set; }

    #endregion
}