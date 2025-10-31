namespace BugStore.Requests.Orders;

public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}