using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Cataglog.Product.Quereis.SearchProducts;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;
using MediatR;

public enum ProductSortBy
{
    Name,
    Price,
    Stock,
    CreatedDate
}

public sealed record SearchProductsQuery(

    string? search,
    Guid? CategoryId,
    Guid? BrandId,
    ProductStatus? Status,
    decimal? MinPrice,
    decimal? MaxPrice,
    bool InStockOnly,
    ProductSortBy SortBy = ProductSortBy.Name,
    bool Descending = false,
    int PageNumber = 1,
    int PageSize = 20)
    : IRequest<PagedResult<ProductSearchResponse>>;

