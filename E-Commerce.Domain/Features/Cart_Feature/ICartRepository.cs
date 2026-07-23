using E_Commerce.Domain.Features.CartFeature.Entities;

namespace E_Commerce.Domain.Features.Cart_Feature;

public interface ICartRepository
{
    Task<Cart?> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken);

    Task<Cart?> GetByIdAsync(
        Guid cartId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Cart cart,
        CancellationToken cancellationToken);

    void Remove(Cart cart);

    Task<bool> ExistsAsync(
        Guid customerId,
        CancellationToken cancellationToken);
}