namespace OrderManagement.Application.Orders;

public class OrderErrors
{
    public static readonly Error NotFound = Error.NotFound("order.not_found", "Order not found.");
}