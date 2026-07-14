using E_Commerce.Domain.Features.ProductFeature.Entites;
using E_Commerce.Domain.Features.ProductFeature.Entities;

namespace E_Commerce.Domain.Features.ProductFeature.Interfaces
{
    public interface IProductRepository
    {
        public Task AddProductAsync(Product product);
        public Task<Product?> GetProductByIdAsync(Guid id);
        public Task<List<Product>> GetAllProductsAsync();

    }
}
