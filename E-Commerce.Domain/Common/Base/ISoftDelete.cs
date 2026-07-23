
namespace E_Commerce.Domain.Common.Base;
    public interface ISoftDelete
    {
        bool IsDeleted { get; }

        DateTime? DeletedAtUtc { get; }

        string? DeletedBy { get; }

        void MarkAsDeleted(string deletedBy);

        void Restore();
    }

  

