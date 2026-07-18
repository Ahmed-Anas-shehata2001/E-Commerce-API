using E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities;

namespace E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;

public interface ICategoryRepository
{
    Task AddCategoryAsync(Category category, CancellationToken cancellationToken);

    Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> CategoryExistsAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> CategoryNameExistsAsync(string name, CancellationToken cancellationToken);

    void DeleteCategory(Category category);

    Task<List<Category>> GetCategoriesAsync(CancellationToken ct);
}