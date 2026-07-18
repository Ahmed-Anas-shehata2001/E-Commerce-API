

using E_Commerce.Domain.Features.Catalog.Exceptions;

namespace E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities;

public sealed class Category : BaseEntity
{
    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    private Category() { }

    public Category(string name, string? description = null)
    {
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidCategoryNameException();

        Name = name.Trim();
    }

    public void SetDescription(string? description)
    {
        Description = description?.Trim();
    }
}