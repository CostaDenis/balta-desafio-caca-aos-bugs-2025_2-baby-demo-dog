using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;

namespace src.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<CreateOrderResponse> CreateAsync(CreateOrderRequest request);
        Task<GetByIdOrderResponse> GetByIdAsync(GetByIdOrderRequest request);
    }
}