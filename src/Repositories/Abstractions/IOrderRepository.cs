using BugStore.Models;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;

namespace src.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order request);
        Task<Order?> GetByIdAsync(Guid id);
    }
}