using E_Commerce.Application.Features.Cataglog.Product;
using E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrandProducts;
using E_Commerce.Application.Features.Catalog.DTOs;
using MediatR;

public sealed class GetBrandProductsQueryHandler
    : IRequestHandler<GetBrandProductsQuery, List<ProductDto>>
{
    private readonly IProductReadRepository _repository;

    public GetBrandProductsQueryHandler(
        IProductReadRepository repository)
    {
        _repository = repository;
    }

    public Task<List<ProductDto>> Handle(
        GetBrandProductsQuery request,
        CancellationToken cancellationToken)
    {
        return _repository.GetProductsByBrandAsync(
            request.BrandId,
            cancellationToken);
    }
}