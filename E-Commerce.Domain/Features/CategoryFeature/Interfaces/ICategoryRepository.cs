using E_Commerce.Domain.Features.CategoryFeature.Entities;

namespace E_Commerce.Domain.Features.CategoryFeature.Interfaces
{
    public interface ICategoryRepository
    {
        public Task AddAsync(Category category, CancellationToken cancellationToken);
        Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);


    }
}
