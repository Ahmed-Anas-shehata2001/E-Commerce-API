using E_Commerce.Domain.Features.AddressFeature.Entities;
using E_Commerce.Domain.Features.AddressFeature.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class AddressRepository
    : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Address?> GetByIdAsync(
        Guid id,
        CancellationToken ct)
    {
        return await _context.Addresses
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<Address>> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken ct)
    {
        return await _context.Addresses
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.IsDefault)
            .ThenBy(x => x.CreatedAtUtc)
            .ToListAsync(ct);
    }

    public async Task<Address?> GetDefaultAsync(
        Guid customerId,
        CancellationToken ct)
    {
        return await _context.Addresses
            .FirstOrDefaultAsync(
                x => x.CustomerId == customerId &&
                     x.IsDefault,
                ct);
    }

    public Task<bool> ExistsAsync(
        Guid id,
        CancellationToken ct)
    {
        return _context.Addresses
            .AnyAsync(x => x.Id == id, ct);
    }

    public async Task AddAsync(
        Address address,
        CancellationToken ct)
    {
        await _context.Addresses.AddAsync(address, ct);
    }

    public void Update(Address address)
    {
        _context.Addresses.Update(address);
    }

    public void Remove(Address address)
    {
        _context.Addresses.Remove(address);
    }
}