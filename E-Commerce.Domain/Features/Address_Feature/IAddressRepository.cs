using E_Commerce.Domain.Features.AddressFeature.Entities;

namespace E_Commerce.Domain.Features.AddressFeature.Interfaces;

public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(Guid id, CancellationToken ct);

    Task<List<Address>> GetByCustomerIdAsync(
        Guid customerId,
       CancellationToken ct);

    Task<Address?> GetDefaultAsync(
        Guid customerId,
        CancellationToken ct);

    Task<bool> ExistsAsync(
        Guid id,
        CancellationToken ct);

    Task AddAsync(
        Address address,
        CancellationToken ct);

    void Update(Address address);

    void Remove(Address address);


}