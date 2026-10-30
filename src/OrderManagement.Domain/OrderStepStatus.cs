namespace OrderManagement.Domain;

public enum OrderStepStatus
{
    Unpaid = 1,
    Paid = 2,
    Rejected = 3,
    Cancelled = 4,
    Returned = 5
}