using E_Commerce.Application.Features.Catalog.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Queries.GetReviewById;

public sealed record GetReviewByIdQuery(Guid Id)
    : IRequest<Result<ReviewDto>>;