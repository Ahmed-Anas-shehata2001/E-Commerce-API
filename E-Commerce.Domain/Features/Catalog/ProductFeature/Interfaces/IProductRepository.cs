using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;

namespace E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product, CancellationToken ct);

        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken ct);

        Task<bool> ProductExistsAsync(Guid id, CancellationToken ct);

        Task<bool> ProductSkuExistsAsync(string sku, CancellationToken ct);

        Task DeleteProduct(Product product);

    }
}
