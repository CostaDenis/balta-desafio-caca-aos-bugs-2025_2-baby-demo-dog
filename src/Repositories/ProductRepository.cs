using BugStore.Data;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;
using src.Repositories.Abstractions;

namespace src.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<Product> CreateAsync(Product request)
        {
            try
            {
                await _context.Products.AddAsync(request);
                await _context.SaveChangesAsync();

                return request;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar produto: {ex.Message}");
            }


        }

        public async Task<List<Product>> GetAsync(int take, int skip)
        {
            try
            {
                return await _context.Products.
                                            AsNoTracking().
                                            Skip(skip).
                                            Take(take).
                                            ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar todos os produtos: {ex.Message}");
            }

        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar produto: {ex.Message}");
            }
        }

        public async Task<Product> Update(Product request)
        {
            try
            {
                var product = await GetByIdAsync(request.Id);

                if (product == null)
                    throw new Exception("Produto não encontrado!");

                product.Id = request.Id;
                product.Title = request.Title;
                product.Description = request.Description;
                product.Slug = request.Slug;
                product.Price = request.Price;

                _context.Products.Update(product);
                _context.SaveChanges();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar produto: {ex.Message}");
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var product = await GetByIdAsync(id);

                if (product == null)
                    throw new Exception("Produto não encontrado!");

                _context.Remove(product);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir produto: {ex.Message}");
            }
        }
    }
}
