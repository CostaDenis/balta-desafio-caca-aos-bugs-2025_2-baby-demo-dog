using BugStore.Models;

namespace src.Repositories.Abstractions
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product request);
        Task<List<Product>> GetAsync(int take, int skip);
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> Update(Product request);
        Task<bool> Delete(Guid id);
    }
}