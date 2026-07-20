using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.ReviewFeature.Entities;
using E_Commerce.Domain.Features.Catalog.ReviewFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Reviews.Commands.CreateReview;

public sealed class CreateReviewCommandHandler
    : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(
            request.ProductId,
            cancellationToken);

        if (product is null)
            throw new ProductNotFoundException(request.ProductId);

        var review = new Review(
            request.ProductId,
            request.CustomerId,
            request.Rating,
            request.Comment);

        await _reviewRepository.AddReviewAsync(
            review,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}