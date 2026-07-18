
namespace E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Entities;
public interface IBrandRepository
{
    Task AddBrandAsync(Brand brand, CancellationToken cancellationToken);

    Task<Brand?> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> BrandExistsAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> BrandNameExistsAsync(string name, CancellationToken cancellationToken);

    void DeleteBrand(Brand brand);

    Task<List<Brand>> GetBrandsAsync(CancellationToken cancellationToken);
}