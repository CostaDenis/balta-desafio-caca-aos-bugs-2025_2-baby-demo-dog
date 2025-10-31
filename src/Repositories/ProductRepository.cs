using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using Microsoft.EntityFrameworkCore;
using src.Repositories.Abstractions;

namespace src.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductReposiory
    {
        private readonly AppDbContext _context = context;
        public async Task<CreateProductResponse> CreateAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Slug = request.Slug,
                Price = request.Price
            };

            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return new CreateProductResponse
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Slug = product.Slug,
                    Price = product.Price
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar produto: {ex.Message}");
            }


        }

        public async Task<List<GetProductResponse>> GetAsync(GetProductRequest request)
        {
            try
            {
                var productResponse = new List<GetProductResponse>();
                var products = await _context.Products.
                                            AsNoTracking().
                                            Skip(request.Skip).
                                            Take(request.Take).
                                            ToListAsync();

                foreach (var product in products)
                {
                    productResponse.Add(new GetProductResponse
                    {
                        Id = product.Id,
                        Title = product.Title,
                        Description = product.Description,
                        Slug = product.Slug,
                        Price = product.Price
                    });
                }

                return productResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar todos os pedidos: {ex.Message}");
            }

        }

        public async Task<GetByIdProductResponse?> GetByIdAsync(GetByIdProductRequest request)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product == null)
                    return null;

                return new GetByIdProductResponse
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Slug = product.Slug,
                    Price = product.Price
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedido: {ex.Message}");
            }
        }

        public async Task<UpdateProductResponse> Update(UpdateProductRequest request)
        {
            try
            {
                var getById = new GetByIdProductRequest
                {
                    Id = request.Id
                };
                var productResponse = await GetByIdAsync(getById);
                var newProduct = new Product();

                if (productResponse == null)
                    throw new Exception("Produto não encontrado!");

                newProduct.Id = request.Id;
                newProduct.Title = request.Title;
                newProduct.Description = request.Description;
                newProduct.Slug = request.Slug;
                newProduct.Price = request.Price;

                _context.Products.Update(newProduct);
                _context.SaveChanges();

                return new UpdateProductResponse
                {
                    Title = newProduct.Title,
                    Description = newProduct.Description,
                    Slug = newProduct.Slug,
                    Price = newProduct.Price
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar produto: {ex.Message}");
            }
        }

        public async Task<DeleteProductResponse> Delete(DeleteProductRequest request)
        {
            try
            {
                var getById = new GetByIdProductRequest
                {
                    Id = request.Id
                };
                var productResponse = await GetByIdAsync(getById);
                var product = new Product();

                if (productResponse == null)
                    throw new Exception("Produto não encontrado!");

                product.Id = productResponse.Id;
                product.Title = productResponse.Title;
                product.Description = productResponse.Description;
                product.Slug = productResponse.Slug;
                product.Price = productResponse.Price;

                _context.Remove(product);
                _context.SaveChanges();

                return new DeleteProductResponse
                {
                    Result = "Pedido excluído com sucesso!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao exclui pedido: {ex.Message}");
            }
        }
    }
}
