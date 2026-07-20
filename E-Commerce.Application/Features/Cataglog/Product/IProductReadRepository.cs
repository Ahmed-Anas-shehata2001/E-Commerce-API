using E_Commerce.Application.Features.Cataglog.Product.Quereis.SearchProducts;
using E_Commerce.Application.Features.Catalog.DTOs;

namespace E_Commerce.Application.Features.Cataglog.Product;
public interface IProductReadRepository
{
    Task<PagedResult<ProductSearchResponse>> SearchProductsAsync(
        SearchProductsQuery query,
        CancellationToken ct);

    Task<List<ProductDto>> GetProductsByBrandAsync(
    Guid brandId,
    CancellationToken cancellationToken);
}