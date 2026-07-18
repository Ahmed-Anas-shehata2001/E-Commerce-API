using E_Commerce.Domain.Features.Catalog.ReviewFeature.Entities;

namespace E_Commerce.Domain.Features.Catalog.ReviewFeature.Interfaces;

public interface IReviewRepository
{
    Task AddReviewAsync(
        Review review,
        CancellationToken cancellationToken);

    Task<Review?> GetReviewByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<bool> ReviewExistsAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<List<Review>> GetProductReviewsAsync(
        Guid productId,
        CancellationToken cancellationToken);

    void DeleteReview(Review review);
}