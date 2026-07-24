using E_Commerce.Domain.Features.OrderFeature.Entities;

namespace E_Commerce.Domain.Features.OrderFeature.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(
        Guid orderId,
        CancellationToken cancellationToken);

    Task<Order?> GetByIdWithItemsAsync(
        Guid orderId,
        CancellationToken cancellationToken);

    Task<List<Order>> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Order order,
        CancellationToken cancellationToken);

    void Remove(Order order);
}