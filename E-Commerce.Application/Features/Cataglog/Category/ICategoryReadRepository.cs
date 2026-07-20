using E_Commerce.Application.Features.Cataglog.Category.DTO;

public interface ICategoryReadRepository
{
    Task<List<CategoryProductDto>> GetCategoryProductsAsync(
        Guid categoryId,
        CancellationToken ct);
}