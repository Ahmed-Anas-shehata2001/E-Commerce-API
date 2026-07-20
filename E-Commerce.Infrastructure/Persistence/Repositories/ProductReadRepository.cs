using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Cataglog.Product;
using E_Commerce.Application.Features.Cataglog.Product.Quereis.SearchProducts;
using E_Commerce.Application.Features.Catalog.DTOs;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class ProductReadRepository : IProductReadRepository
{
    private readonly ApplicationDbContext _context;

    public ProductReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ProductSearchResponse>> SearchProductsAsync(
        SearchProductsQuery filter,
        CancellationToken ct)
    {
        IQueryable<Product> query = _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Brand);

        if (!string.IsNullOrWhiteSpace(filter.search))
        {
            var keyword = $"%{filter.search.Trim()}%";

            query = query.Where(p =>
                EF.Functions.Like(p.Name, keyword) ||
                EF.Functions.Like(p.SKU, keyword));
        }

        if (filter.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == filter.CategoryId);

        if (filter.BrandId.HasValue)
            query = query.Where(p => p.BrandId == filter.BrandId);

        if (filter.Status.HasValue)
            query = query.Where(p => p.Status == filter.Status);

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filter.MinPrice);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice);

        if (filter.InStockOnly)
            query = query.Where(p => p.Stock > 0);

        var totalCount = await query.CountAsync(ct);

        // Apply sorting here
        query = (filter.SortBy, filter.Descending) switch
        {
            (ProductSortBy.Name, false) => query.OrderBy(p => p.Name),
            (ProductSortBy.Name, true) => query.OrderByDescending(p => p.Name),

            (ProductSortBy.Price, false) => query.OrderBy(p => p.Price),
            (ProductSortBy.Price, true) => query.OrderByDescending(p => p.Price),

            (ProductSortBy.Stock, false) => query.OrderBy(p => p.Stock),
            (ProductSortBy.Stock, true) => query.OrderByDescending(p => p.Stock),

            (ProductSortBy.CreatedDate, false) => query.OrderBy(p => p.CreatedAtUtc),
            (ProductSortBy.CreatedDate, true) => query.OrderByDescending(p => p.CreatedAtUtc),

            _ => query.OrderBy(p => p.Name)
        };

        var items = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(p => new ProductSearchResponse(
                p.Id,
                p.Name,
                p.SKU,
                p.Price,
                p.CurrentPrice,
                p.Stock,
                p.Status,
                p.CategoryId,
                p.Category.Name,
                p.BrandId,
                p.Brand.Name))
            .ToListAsync(ct);

        return new PagedResult<ProductSearchResponse>()
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize,

        };
    }

    public async Task<List<ProductDto>> GetProductsByBrandAsync(
    Guid brandId,
    CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.BrandId == brandId)
            .OrderBy(p => p.Name)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                CreatedAtUTC = p.CreatedAtUtc,
                UpdatedAtUTC = p.UpdatedAtUtc,
                SKU = p.SKU,
                BrandName = p.Brand.Name,

            })
            .ToListAsync(cancellationToken);
    }


    public async Task<PagedResult<ProductSearchResponse>> GetSellerProductsAsync(
    Guid sellerId,
    int pageNumber,
    int pageSize,
    CancellationToken ct)
    {
        var query = _context.Products
            .AsNoTracking()
            .Where(p => p.SellerId == sellerId)
            .Include(p => p.Category)
            .Include(p => p.Brand);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderBy(p => p.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductSearchResponse(
                p.Id,
                p.Name,
                p.SKU,
                p.Price,
                p.CurrentPrice,
                p.Stock,
                p.Status,
                p.CategoryId,
                p.Category.Name,
                p.BrandId,
                p.Brand.Name))
            .ToListAsync(ct);

        return new PagedResult<ProductSearchResponse>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

    }

}