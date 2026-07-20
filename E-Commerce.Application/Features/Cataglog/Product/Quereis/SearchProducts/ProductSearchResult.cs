

using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;

namespace E_Commerce.Application.Features.Cataglog.Product.Quereis.SearchProducts
{
    public sealed record ProductSearchResult(
    Guid Id,
    string Name,
    string SKU,
    decimal Price,
    decimal CurrentPrice,
    bool HasDiscount,
    int Stock,
    ProductStatus Status,
    Guid CategoryId,
    Guid BrandId);


}
