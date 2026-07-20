using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ReviewFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Commands.DeleteReview;

public sealed class DeleteReviewCommandHandler
    : IRequestHandler<DeleteReviewCommand, Result>
{
    private readonly IReviewRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReviewCommandHandler(
        IReviewRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteReviewCommand request,
        CancellationToken cancellationToken)
    {
        var review = await _repository.GetReviewByIdAsync(
            request.Id,
            cancellationToken);

        if (review is null)
            return Result.Failure("Review not found.");

        _repository.DeleteReview(review);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}