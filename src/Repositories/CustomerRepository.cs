using BugStore.Data;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;
using src.Repositories.Abstractions;

namespace src.Repositories
{
    public class CustomerRepository(AppDbContext context) : ICustomerRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Customer> CreateAsync(Customer request)
        {
            try
            {
                await _context.Customers.AddAsync(request);
                await _context.SaveChangesAsync();

                return request;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar cliente: {ex.Message}");
            }

        }

        public async Task<List<Customer>> GetAsync(int take, int skip)
        {
            try
            {
                return await _context.Customers.
                                                AsNoTracking().
                                                Skip(skip).
                                                Take(take).
                                                ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar todos os clientes: {ex.Message}");
            }

        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar um cliente: {ex.Message}");
            }

        }

        public async Task<Customer> Update(Customer request)
        {
            try
            {
                var customer = await GetByIdAsync(request.Id);

                if (customer == null)
                    throw new Exception("Cliente não encontrado!");

                customer.Name = request.Name;
                customer.Email = request.Email;
                customer.Phone = request.Phone;
                customer.BirthDate = request.BirthDate;

                _context.Customers.Update(customer);
                _context.SaveChanges();

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar cliente: {ex.Message}");
            }

        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var customer = await GetByIdAsync(id);


                if (customer == null)
                    throw new Exception("Cliente não encontrado");

                _context.Customers.Remove(customer);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar cliente: {ex.Message}");
            }

        }
    }
}