using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.CreateProduct;

public sealed class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _products;
    private readonly ICategoryRepository _categories;
    private readonly IBrandRepository _brands;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository products,
        ICategoryRepository categories,
        IBrandRepository brands,
        IUnitOfWork unitOfWork)
    {
        _products = products;
        _categories = categories;
        _brands = brands;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _categories.CategoryExistsAsync(request.CategoryId, cancellationToken))
            throw new CategoryNotFoundException(request.CategoryId);

        if (!await _brands.BrandExistsAsync(request.BrandId, cancellationToken))
            throw new BrandNotFoundException(request.BrandId);

        if (await _products.ProductSkuExistsAsync(request.SKU, cancellationToken))
            throw new DuplicateProductSkuException(request.SKU);

        var product = new Domain.Features.Catalog.ProductFeature.Entities.Product(
            request.Name,
            request.SKU,
            request.Price,
            request.Stock,
            request.CategoryId,
            request.BrandId,
            request.SellerId,
            request.Description,
            request.Weight);

        await _products.AddProductAsync(product, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}