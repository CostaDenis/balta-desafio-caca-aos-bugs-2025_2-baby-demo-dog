using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;

namespace src.Repositories.Abstractions
{
    public interface ICustomerRepository
    {
        Task<CreateCustomerResponse> CreateAsync(CreateCustomerRequest request);
        Task<List<GetCustomerResponse>> GetAsync(GetCustomerRequest request);
        Task<GetByIdCustomerResponse> GetByIdAsync(GetByIdCustomerRequest request);
        Task<UpdateCustomerResponse> Update(UpdateCustomerRequest request);
        Task<DeleteCustomerResponse> Delete(DeleteCustomerRequest request);
    }
}