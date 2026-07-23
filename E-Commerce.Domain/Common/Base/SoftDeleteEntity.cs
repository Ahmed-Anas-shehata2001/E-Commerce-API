using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Base
{
    public abstract class SoftDeleteEntity
    : AuditableEntity, ISoftDelete
    {
        public DateTime? DeletedAtUtc { get; protected set; }

        public string? DeletedBy { get; protected set; }

        public bool IsDeleted => DeletedAtUtc.HasValue;

        public void MarkAsDeleted(string deletedBy)
        {
            if (IsDeleted)
                return;

            DeletedAtUtc = DateTime.UtcNow;
            DeletedBy = deletedBy;
        }

        public void Restore()
        {
            DeletedAtUtc = null;
            DeletedBy = null;
        }
    }
}
