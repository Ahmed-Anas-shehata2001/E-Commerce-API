using E_Commerce.Application.Features.Cataglog.Product.DTO;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.Errors;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Cataglog.Product.Quereis.GetProductById
{
    public class GetProductByIdHandler
       : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductDto>> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);

            if (product is null)
                return Result<ProductDto>.Failure(
                    ProductErrors.NotFound(request.Id));

            return Result<ProductDto>.Success(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            });
        }
    }
}
