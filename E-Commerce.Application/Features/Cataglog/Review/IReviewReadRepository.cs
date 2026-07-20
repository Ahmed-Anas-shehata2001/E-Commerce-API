using E_Commerce.Application.Features.Catalog.DTOs;


namespace E_Commerce.Application.Features.Catalog.Reviews;

public interface IReviewReadRepository
{
    Task<ReviewDto?> GetReviewByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<List<ReviewDto>> GetProductReviewsAsync(
        Guid productId,
        CancellationToken cancellationToken);
}