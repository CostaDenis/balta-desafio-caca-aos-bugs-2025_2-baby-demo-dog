namespace BugStore.Models;

public class OrderLine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }

    public int Quantity { get; set; }
    public decimal Total { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = new();
}