using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.UnpublishProduct;

public sealed class UnpublishProductCommandHandler
    : IRequestHandler<UnpublishProductCommand, Result>
{
    private readonly IProductRepository _products;
    private readonly IUnitOfWork _unitOfWork;

    public UnpublishProductCommandHandler(
        IProductRepository products,
        IUnitOfWork unitOfWork)
    {
        _products = products;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UnpublishProductCommand request,
        CancellationToken ct)
    {
        var product = await _products.GetProductByIdAsync(request.ProductId, ct)
            ?? throw new ProductNotFoundException(request.ProductId);

        product.Archive();

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}