using E_Commerce.Application.Features.Catalog.DTOs;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Queries.GetProductReviews;

public sealed record GetProductReviewsQuery(Guid ProductId)
    : IRequest<List<ReviewDto>>;