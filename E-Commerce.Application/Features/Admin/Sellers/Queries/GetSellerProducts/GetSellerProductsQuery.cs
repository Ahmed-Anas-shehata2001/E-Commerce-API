using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Cataglog.Product.Quereis.SearchProducts;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerProducts;

public sealed record GetSellerProductsQuery(
    Guid SellerId,
    int PageNumber = 1,
    int PageSize = 20)
    : IRequest<PagedResult<ProductSearchResponse>>;