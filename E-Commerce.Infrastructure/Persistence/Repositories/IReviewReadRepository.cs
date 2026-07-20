using E_Commerce.Application.Features.Catalog.DTOs;
using E_Commerce.Application.Features.Catalog.Reviews;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class ReviewReadRepository : IReviewReadRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReviewDto?> GetReviewByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                CustomerId = r.CustomerId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAtUtc = r.CreatedAtUtc,
                UpdatedAtUtc = r.UpdatedAtUtc
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ReviewDto>> GetProductReviewsAsync(
        Guid productId,
        CancellationToken cancellationToken)
    {
        return await _context.Reviews
            .AsNoTracking()
            .Where(r => r.ProductId == productId)
            .OrderByDescending(r => r.CreatedAtUtc)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                CustomerId = r.CustomerId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAtUtc = r.CreatedAtUtc,
                UpdatedAtUtc = r.UpdatedAtUtc
            })
            .ToListAsync(cancellationToken);
    }
}