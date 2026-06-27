using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Interfaces
{
    public interface  IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
