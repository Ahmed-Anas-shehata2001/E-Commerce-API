using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Commands.UpdateReview;

public sealed record UpdateReviewCommand(
    Guid Id,
    int Rating,
    string? Comment)
    : IRequest<Result>;