using E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCategoryAsync(
        Category category,
        CancellationToken ct)
    {
        await _context.Categories.AddAsync(category, ct);
    }

    public async Task<Category?> GetCategoryByIdAsync(
        Guid id,
        CancellationToken ct)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<bool> CategoryExistsAsync(
        Guid id,
        CancellationToken ct)
    {
        return await _context.Categories
            .AnyAsync(c => c.Id == id, ct);
    }

    public async Task<bool> CategoryNameExistsAsync(
        string name,
        CancellationToken ct)
    {
        name = name.Trim().ToUpperInvariant();

        return await _context.Categories
            .AnyAsync(c => c.Name == name, ct);
    }

    public async Task<List<Category>> GetCategoriesAsync(
        CancellationToken ct)
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(ct);
    }

    public void DeleteCategory(Category category)
    {
        _context.Categories.Remove(category);
    }
}