using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;


namespace E_Commerce.Application.Features.Cataglog.Product.Commands.ApplyDiscount
{
    public sealed class ApplyDiscountCommandHandler : IRequestHandler<ApplyDiscountCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplyDiscountCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ApplyDiscountCommand request, CancellationToken ct)
        {
            var product = await _productRepository.GetProductByIdAsync(request.ProductId, ct)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.ApplyDiscount(request.Percentage, request.StartDateUtc, request.EndDateUtc);

            await _unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }
    }
}
