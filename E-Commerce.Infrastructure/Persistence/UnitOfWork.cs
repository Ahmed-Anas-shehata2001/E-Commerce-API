

using E_Commerce.Domain.Common.Interfaces;

namespace E_Commerce.Infrastructure.Persistence
{

    // I created just a wrapper to DbContext. 
    // DbContext itself
    //    Already does:
    //change tracking
    //transaction coordination
    //commit all changes together
    //👉 That IS Unit of Work behavior.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
