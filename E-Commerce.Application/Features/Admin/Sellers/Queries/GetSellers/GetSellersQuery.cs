using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellers;

public sealed class SellerDto
{
    public Guid Id { get; init; }

    public string UserName { get; init; } = default!;

    public string Email { get; init; } = default!;

    public string FirstName { get; init; } = default!;

    public string LastName { get; init; } = default!;

    public bool IsActive { get; init; }

    public bool IsDeleted { get; init; }

    public DateTime CreatedAt { get; init; }
}

public sealed record GetSellersQuery(
    int PageNumber = 1,
    int PageSize = 20)
    : IRequest<Result<PagedResult<SellerDto>>>;