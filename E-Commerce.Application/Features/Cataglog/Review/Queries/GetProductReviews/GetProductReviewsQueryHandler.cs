using E_Commerce.Application.Features.Catalog.DTOs;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Queries.GetProductReviews;

public sealed class GetProductReviewsQueryHandler
    : IRequestHandler<GetProductReviewsQuery, List<ReviewDto>>
{
    private readonly IReviewReadRepository _repository;

    public GetProductReviewsQueryHandler(
        IReviewReadRepository repository)
    {
        _repository = repository;
    }

    public Task<List<ReviewDto>> Handle(
        GetProductReviewsQuery request,
        CancellationToken cancellationToken)
    {
        return _repository.GetProductReviewsAsync(
            request.ProductId,
            cancellationToken);
    }
}