using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Cataglog.Product;
using E_Commerce.Application.Features.Cataglog.Product.Quereis.SearchProducts;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerProducts;

public sealed class GetSellerProductsQueryHandler
    : IRequestHandler<GetSellerProductsQuery, PagedResult<ProductSearchResponse>>
{
    private readonly IProductReadRepository _repository;

    public GetSellerProductsQueryHandler(
        IProductReadRepository repository)
    {
        _repository = repository;
    }

    public Task<PagedResult<ProductSearchResponse>> Handle(
        GetSellerProductsQuery request,
        CancellationToken cancellationToken)
    {
        return _repository.GetSellerProductsAsync(
            request.SellerId,
            request.PageNumber,
            request.PageSize,
            cancellationToken);
    }
}