using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.UpdateProductDetails;

public sealed class UpdateProductDetailsCommandHandler
    : IRequestHandler<UpdateProductDetailsCommand, Result>
{
    private readonly IProductRepository _products;
    private readonly ICategoryRepository _categories;
    private readonly IBrandRepository _brands;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductDetailsCommandHandler(
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

    public async Task<Result> Handle(
        UpdateProductDetailsCommand request,
        CancellationToken ct)
    {
        var product = await _products.GetProductByIdAsync(request.ProductId, ct)
            ?? throw new ProductNotFoundException(request.ProductId);

        if (!await _categories.CategoryExistsAsync(request.CategoryId, ct))
            throw new CategoryNotFoundException(request.CategoryId);

        if (!await _brands.BrandExistsAsync(request.BrandId, ct))
            throw new BrandNotFoundException(request.BrandId);

        product.SetName(request.Name);
        product.SetDescription(request.Description);
        product.SetWeight(request.Weight);
        product.AssignCategory(request.CategoryId);
        product.AssignBrand(request.BrandId);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}