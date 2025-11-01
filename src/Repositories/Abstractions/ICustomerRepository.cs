using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;

namespace src.Repositories.Abstractions
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(Customer request);
        Task<List<Customer>> GetAsync(int take, int skip);
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer> Update(Customer request);
        Task<bool> Delete(Guid id);
    }
}