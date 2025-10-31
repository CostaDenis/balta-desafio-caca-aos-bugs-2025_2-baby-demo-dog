using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using Microsoft.EntityFrameworkCore;
using src.Repositories.Abstractions;

namespace src.Repositories
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<CreateOrderResponse> CreateAsync(CreateOrderRequest request)
        {
            var order = new Order
            {
                CustomerId = request.CustomerId,
                CreatedAt = request.CreatedAt
            };

            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                return new CreateOrderResponse
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    CreatedAt = order.CreatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar pedido: {ex.Message}");
            }

        }

        public async Task<GetByIdOrderResponse?> GetByIdAsync(GetByIdOrderRequest request)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
                return null;

            return new GetByIdOrderResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CreatedAt = order.CreatedAt,
                Lines = order.Lines
            };
        }
    }
}