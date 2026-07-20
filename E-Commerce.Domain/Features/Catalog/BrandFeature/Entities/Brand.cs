using E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities;
using E_Commerce.Domain.Features.Catalog.Exceptions;

namespace E_Commerce.Domain.Features.Catalog.BrandFeature.Entities;

public enum BrandStatus
{
    Active,
    Archived
}
public class Brand : BaseEntity
{
    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    public BrandStatus Status { get; private set; } = BrandStatus.Active;



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

    public void Update(
    string name,
    string? description)
    {
        SetName(name);
        SetDescription(description);

        Touch();
    }



    public void Archive()
    {
        if (Status == BrandStatus.Archived)
            throw new BrandAlreadyArchivedException();

        Status = BrandStatus.Archived;
        Touch();
    }

    public void UnArchive()
    {
        if (Status == BrandStatus.Active)
            throw new BrandAlreadyActiveException();

        Status = BrandStatus.Active;
        Touch();
    }
 
 

    private void Touch()
    {
        UpdatedAtUtc = DateTime.UtcNow;
    }

}