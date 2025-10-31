namespace BugStore.Requests.Customers;

public class GetCustomerRequest
{
    public int Take { get; set; } = 25;
    public int Skip { get; set; } = 0;
}