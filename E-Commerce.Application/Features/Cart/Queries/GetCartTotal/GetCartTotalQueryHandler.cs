using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Features.Cart_Feature;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Queries.GetCartTotal;

public sealed class GetCartTotalQueryHandler
    : IRequestHandler<GetCartTotalQuery, decimal>
{
    private readonly ICartRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetCartTotalQueryHandler(
        ICartRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<decimal> Handle(
        GetCartTotalQuery request,
        CancellationToken ct)
    {
        var cart = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId!.Value,
            ct);

        return cart?.CalculateTotalPrice() ?? 0m;
    }
}