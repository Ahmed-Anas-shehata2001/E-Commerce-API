using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Commands.CreateReview;

public sealed record CreateReviewCommand(
    Guid ProductId,
    Guid CustomerId,
    int Rating,
    string? Comment)
    : IRequest<Guid>;