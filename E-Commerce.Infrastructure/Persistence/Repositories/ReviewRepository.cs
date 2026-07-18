using E_Commerce.Domain.Features.Catalog.ReviewFeature.Entities;
using E_Commerce.Domain.Features.Catalog.ReviewFeature.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddReviewAsync(
        Review review,
        CancellationToken cancellationToken)
    {
        await _context.Reviews.AddAsync(review, cancellationToken);
    }

    public async Task<Review?> GetReviewByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<bool> ReviewExistsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .AnyAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<List<Review>> GetProductReviewsAsync(
        Guid productId,
        CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .Where(r => r.ProductId == productId)
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public void DeleteReview(Review review)
    {
        _context.Reviews.Remove(review);
    }
}