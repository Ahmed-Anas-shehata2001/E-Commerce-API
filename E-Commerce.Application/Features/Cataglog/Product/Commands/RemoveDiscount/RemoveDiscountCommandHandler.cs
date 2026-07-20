using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Cataglog.Product.Commands.RemoveDiscount
{
    public sealed class RemoveDiscountCommandHandler : IRequestHandler<RemoveDiscountCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveDiscountCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveDiscountCommand request, CancellationToken ct)
        {
            var product = await _productRepository.GetProductByIdAsync(request.ProductId, ct)
                ?? throw new ProductNotFoundException(request.ProductId);

            product.RemoveDiscount();

            await _unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }
    }
}
