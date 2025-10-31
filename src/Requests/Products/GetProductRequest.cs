namespace BugStore.Requests.Products;

public class GetProductRequest
{
    public int Take { get; set; } = 25;
    public int Skip { get; set; } = 0;
}