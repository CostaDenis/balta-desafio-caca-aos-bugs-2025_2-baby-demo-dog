using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugStore.Requests.Products;
using BugStore.Responses.Products;

namespace src.Repositories.Abstractions
{
    public interface IProductReposiory
    {
        Task<CreateProductResponse> CreateAsync(CreateProductRequest request);
        Task<List<GetProductResponse>> GetAsync(GetProductRequest request);
        Task<GetByIdProductResponse?> GetByIdAsync(GetByIdProductRequest request);
        Task<UpdateProductResponse> Update(UpdateProductRequest request);
        Task<DeleteProductResponse> Delete(DeleteProductRequest request);
    }
}