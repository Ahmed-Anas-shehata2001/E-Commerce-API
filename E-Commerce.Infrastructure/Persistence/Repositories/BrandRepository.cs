using E_Commerce.Domain.Features.Catalog.BrandFeature.Entities;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddBrandAsync(
        Brand brand,
        CancellationToken cancellationToken)
    {
        await _context.Brands.AddAsync(brand, cancellationToken);
    }

    public async Task<Brand?> GetBrandByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Brands
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<bool> BrandExistsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Brands
            .AnyAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<bool> BrandNameExistsAsync(
        string name,
        CancellationToken cancellationToken)
    {
        name = name.Trim();

        return await _context.Brands
            .AnyAsync(b => b.Name == name, cancellationToken);
    }

    public async Task<List<Brand>> GetBrandsAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Brands
            .AsNoTracking()
            .OrderBy(b => b.Name)
            .ToListAsync(cancellationToken);
    }

    public void DeleteBrand(Brand brand)
    {
        _context.Brands.Remove(brand);
    }

 
}