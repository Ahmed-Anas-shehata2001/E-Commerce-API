using E_Commerce.Application.Features.Catalog.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Queries.GetReviewById;

public sealed class GetReviewByIdQueryHandler
    : IRequestHandler<GetReviewByIdQuery, Result<ReviewDto>>
{
    private readonly IReviewReadRepository _repository;

    public GetReviewByIdQueryHandler(
        IReviewReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ReviewDto>> Handle(
        GetReviewByIdQuery request,
        CancellationToken cancellationToken)
    {
        var review = await _repository.GetReviewByIdAsync(
            request.Id,
            cancellationToken);

        if (review is null)
            return Result<ReviewDto>.Failure("Review not found.");

        return Result<ReviewDto>.Success(review);
    }
}