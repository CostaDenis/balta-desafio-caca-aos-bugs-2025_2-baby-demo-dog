using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using Microsoft.EntityFrameworkCore;
using src.Repositories.Abstractions;

namespace src.Repositories
{
    public class CustomerRepository(AppDbContext context) : ICustomerRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<CreateCustomerResponse> CreateAsync(CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                BirthDate = request.BirthDate
            };

            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                return new CreateCustomerResponse
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    BirthDate = customer.BirthDate
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar cliente: {ex.Message}");
            }

        }

        public async Task<List<GetCustomerResponse>> GetAsync(GetCustomerRequest request)
        {
            try
            {
                var customersResponse = new List<GetCustomerResponse>();
                var customers = await _context.Customers.
                                                AsNoTracking().
                                                Skip(request.Skip).
                                                Take(request.Take).
                                                ToListAsync();

                foreach (var customer in customers)
                {
                    customersResponse.Add(
                        new GetCustomerResponse
                        {
                            Id = customer.Id,
                            Name = customer.Name,
                            Email = customer.Email,
                            Phone = customer.Phone,
                            BirthDate = customer.BirthDate
                        }
                    );
                }

                return customersResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar todos os clientes: {ex.Message}");
            }

        }

        public async Task<GetByIdCustomerResponse?> GetByIdAsync(GetByIdCustomerRequest request)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (customer == null)
                    return null;

                return new GetByIdCustomerResponse
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    BirthDate = customer.BirthDate
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar um cliente: {ex.Message}");
            }

        }

        public async Task<UpdateCustomerResponse> Update(UpdateCustomerRequest request)
        {
            var getById = new GetByIdCustomerRequest
            {
                Id = request.Id
            };

            try
            {
                var customerResponse = await GetByIdAsync(getById);
                var newCustomer = new Customer();

                if (customerResponse == null)
                    throw new Exception("Cliente não encontrado");

                newCustomer.Id = request.Id;
                newCustomer.Name = request.Name;
                newCustomer.Email = request.Email;
                newCustomer.Phone = request.Phone;
                newCustomer.BirthDate = request.BirthDate;

                _context.Customers.Update(newCustomer);
                _context.SaveChanges();

                return new UpdateCustomerResponse
                {
                    Name = newCustomer.Name,
                    Email = newCustomer.Email,
                    Phone = newCustomer.Phone,
                    BirthDate = newCustomer.BirthDate
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar cliente: {ex.Message}");
            }

        }

        public async Task<DeleteCustomerResponse> Delete(DeleteCustomerRequest request)
        {
            var getById = new GetByIdCustomerRequest
            {
                Id = request.Id
            };

            try
            {
                var customerResponse = await GetByIdAsync(getById);
                var customer = new Customer();

                if (customerResponse == null)
                    throw new Exception("Cliente não encontrado");

                customer.Id = customerResponse.Id;
                customer.Name = customerResponse.Name;
                customer.Email = customerResponse.Email;
                customer.Phone = customerResponse.Phone;
                customer.BirthDate = customerResponse.BirthDate;

                _context.Customers.Remove(customer);
                _context.SaveChanges();

                return new DeleteCustomerResponse
                {
                    Result = "Cliente excluído com sucesso!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar cliente: {ex.Message}");
            }

        }
    }
}