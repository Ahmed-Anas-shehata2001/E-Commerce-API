

using E_Commerce.Domain.Features.Catalog.Exceptions;

namespace E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities;

public enum CategoryStatus
{
    Active = 1,
    Archived = 2
}

public sealed class Category : BaseEntity
{
    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    public CategoryStatus Status { get; private set; } = CategoryStatus.Active;
    private Category() { }

    public Category(string name, string? description = null)
    {
        SetName(name);
        SetDescription(description);
    }



    public void Update(
    string name,
    string? description)
    {
        SetName(name);
        SetDescription(description);

        UpdatedAtUtc = DateTime.UtcNow;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidCategoryNameException();

        Name = name.Trim();
    }

    private void SetDescription(string? description)
    {
        Description = description?.Trim();
    }

    public void Archive()
    {
        if (Status == CategoryStatus.Archived)
            throw new CategoryAlreadyArchivedException();

        Status = CategoryStatus.Archived;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void UnArchive()
    {
        if (Status == CategoryStatus.Active)
            throw new CategoryAlreadyActiveException();

        Status = CategoryStatus.Active;
        UpdatedAtUtc = DateTime.UtcNow;
    }


}