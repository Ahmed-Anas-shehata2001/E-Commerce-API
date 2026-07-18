public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    // Auditing and soft delete
    public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtUtc { get; protected set; }

    public string? CreatedBy { get; protected set; }

    public string? UpdatedBy { get; protected set; }
    public byte[]? RowVersion { get; set; }     // optimistic concurrency


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

    protected void MarkAsUpdated(string updatedBy)
    {
        UpdatedAtUtc = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}