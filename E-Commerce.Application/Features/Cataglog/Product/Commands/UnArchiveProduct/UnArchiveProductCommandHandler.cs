using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

public sealed class UnArchiveProductCommandHandler
    : IRequestHandler<UnArchiveProductCommand, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnArchiveProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UnArchiveProductCommand request,
        CancellationToken ct)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId, ct)
            ?? throw new ProductNotFoundException(request.ProductId);

        product.UnArchive();

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}