using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddProductAsync(
        Product product,
        CancellationToken ct)
    {
        await _context.Products.AddAsync(product, ct);
    }

    public async Task<Product?> GetProductByIdAsync(
        Guid id,
        CancellationToken ct)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<bool> ProductExistsAsync(
        Guid id,
        CancellationToken ct)
    {
        return await _context.Products
            .AnyAsync(p => p.Id == id, ct);
    }

    public async Task<bool> ProductSkuExistsAsync(
        string sku,
        CancellationToken ct)
    {
        sku = sku.Trim().ToUpperInvariant();

        return await _context.Products
            .AnyAsync(p => p.SKU == sku, ct);
    }

    public Task DeleteProduct(Product product)
    {
        _context.Products.Remove(product);

        return Task.CompletedTask;
    }



}