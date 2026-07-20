using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerStatisitics;
using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed record GetSellerStatisticsQuery(Guid SellerId)
    : IRequest<Result<SellerStatisticsDto>>;