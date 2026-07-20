using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Commands.DeleteReview;

public sealed record DeleteReviewCommand(Guid Id)
    : IRequest<Result>;