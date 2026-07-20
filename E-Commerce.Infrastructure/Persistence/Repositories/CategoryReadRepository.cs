using E_Commerce.Application.Features.Cataglog.Category.DTO;
using E_Commerce.Domain.Features.Catalog.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Persistence.Repositories
{
    public class CategoryReadRepository  : ICategoryReadRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryProductDto>> GetCategoryProductsAsync(
    Guid categoryId,
    CancellationToken ct)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p =>
                    p.CategoryId == categoryId &&
                    p.Status == ProductStatus.Published)
                .OrderBy(p => p.Name)
                .Select(p => new CategoryProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SKU = p.SKU,
                    Price = p.Price,
                    currPrice = p.CurrentPrice,
                    Stock = p.Stock
                })
                .ToListAsync(ct);
        }
    }
}
