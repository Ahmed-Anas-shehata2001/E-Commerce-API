using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Application.Features.Catalog.Commands.PublishProduct;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

public sealed class PublishProductCommandHandler
    : IRequestHandler<PublishProductCommand, Result>
{
    private readonly IProductRepository _products;
    private readonly IUnitOfWork _unitOfWork;

    public PublishProductCommandHandler(
        IProductRepository products,
        IUnitOfWork unitOfWork)
    {
        _products = products;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        PublishProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _products.GetProductByIdAsync(
            request.ProductId,
            cancellationToken);

        if (product is null)
            throw new ProductNotFoundException(request.ProductId);

        product.UnArchive();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}