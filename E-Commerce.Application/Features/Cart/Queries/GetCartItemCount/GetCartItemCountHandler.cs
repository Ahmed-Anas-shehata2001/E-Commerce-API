using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Features.Cart_Feature;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Queries.GetCartItemCount;

public sealed class GetCartItemCountQueryHandler
    : IRequestHandler<GetCartItemCountQuery, int>
{
    private readonly ICartRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetCartItemCountQueryHandler(
        ICartRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(
        GetCartItemCountQuery request,
        CancellationToken ct)
    {
        var cart = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId!.Value,
            ct);

      return cart?.CalculateItemsCount() ?? 0;
    }
}