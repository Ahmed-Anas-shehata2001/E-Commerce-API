
using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;

namespace E_Commerce.Application.Features.Cataglog.Product.Quereis.SearchProducts;

public sealed record ProductSearchResponse(
    Guid Id,
    string Name,
    string SKU,
    decimal Price,
    decimal CurrentPrice,
    int Stock,
    ProductStatus Status,
    Guid CategoryId,
    string CategoryName,
    Guid BrandId,
    string BrandName);