using E_Commerce.Application.Features.Cataglog.Product;
using E_Commerce.Application.Features.Cataglog.Product.Quereis.SearchProducts;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Product.Queries.SearchProducts;

public sealed class SearchProductsQueryHandler
    : IRequestHandler<SearchProductsQuery, PagedResult<ProductSearchResponse>>
{
    private readonly IProductReadRepository _repository;

    public SearchProductsQueryHandler(
        IProductReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<ProductSearchResponse>> Handle(
        SearchProductsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.SearchProductsAsync(
            request,
            cancellationToken);
    }
}