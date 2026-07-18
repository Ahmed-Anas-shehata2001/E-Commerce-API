using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.UpdateProductPrice;

public sealed class UpdateProductPriceCommandHandler
    : IRequestHandler<UpdateProductPriceCommand, Result>
{
    private readonly IProductRepository _products;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductPriceCommandHandler(
        IProductRepository products,
        IUnitOfWork unitOfWork)
    {
        _products = products;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateProductPriceCommand request,
        CancellationToken ct)
    {
        var product = await _products.GetProductByIdAsync(request.ProductId, ct)
            ?? throw new ProductNotFoundException(request.ProductId);

        product.SetPrice(request.Price);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}