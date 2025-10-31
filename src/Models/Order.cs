namespace BugStore.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public List<OrderLine> Lines { get; set; } = new List<OrderLine>();
}