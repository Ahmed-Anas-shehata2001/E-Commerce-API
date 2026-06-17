using E_Commerce.Domain.Features.CategoryFeature.Entities;
using E_Commerce.Domain.Features.CategoryFeature.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Categories.Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking() // improves performance for read-only queries
                .ToListAsync(cancellationToken);
        }


    }
}
