using E_Commerce.Domain.Features.Catalog.Exceptions;

namespace E_Commerce.Domain.Features.Catalog.BrandFeature.Entities;

public class Brand : BaseEntity
{
    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    private Brand() { }

    public Brand(string name, string? description = null)
    {
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidBrandNameException();

        Name = name.Trim();
    }

    public void SetDescription(string? description)
    {
        Description = description?.Trim();
    }
}