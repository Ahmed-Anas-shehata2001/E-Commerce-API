using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellers;
using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed record GetSellerByIdQuery(Guid SellerId)
    : IRequest<Result<SellerDto>>;