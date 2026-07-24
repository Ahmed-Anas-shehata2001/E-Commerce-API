
using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Common.Exceptions;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Entities;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities;
using E_Commerce.Domain.Features.Catalog.ReviewFeature.Entities;

namespace E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;

public enum ProductStatus
{

    Published,
    Archived
}

public sealed class Product : SoftDeleteEntity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public string SKU { get; private set; } = default!;

    public decimal Price { get; private set; }

    public int Stock { get; private set; }

    public decimal? Weight { get; private set; }

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = default!;

    public Guid BrandId { get; private set; }
    public Brand Brand { get; private set; } = default!;

    public Guid SellerId { get; private set; }

    public ProductStatus Status { get; private set; } = ProductStatus.Published;

    public decimal? DiscountPercentage { get; private set; }

    public DateTime? DiscountStartDateUtc { get; private set; }

    public DateTime? DiscountEndDateUtc { get; private set; }

    private readonly List<Review> _reviews = new();
    public IReadOnlyCollection<Review> Reviews => _reviews;

    public decimal CurrentPrice =>
    HasDiscount
        ? Price * (1 - DiscountPercentage!.Value / 100m)
        : Price;
    public bool HasDiscount =>
       DiscountPercentage.HasValue &&
       DiscountStartDateUtc.HasValue &&
       DiscountEndDateUtc.HasValue &&
       DiscountStartDateUtc <= DateTime.UtcNow &&
       DiscountEndDateUtc >= DateTime.UtcNow;



    private Product() { }

    // business rules
    public Product(
        string name,
        string sku,
        decimal price,
        int stock,
        Guid categoryId,
        Guid brandId,
        Guid sellerId,
        string? description = null,
        decimal? weight = null)
    {
        SetName(name);
        SetSku(sku);
        SetDescription(description);
        SetPrice(price);
        SetInitialStock(stock);
        SetWeight(weight);

        AssignCategory(categoryId);
        AssignBrand(brandId);

        SellerId = sellerId;
    }

    private void SetInitialStock(int stock)
    {
        if (stock < 0)
            throw new InvalidStockException();

        Stock = stock;
    }
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidProductNameException();

        Name = name.Trim();
    }

    public void SetSku(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new Exception("SKU is required.");

        SKU = sku.Trim().ToUpperInvariant();
    }

    public void SetDescription(string? description)
    {
        Description = description?.Trim();
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new InvalidPriceException();

        Price = price;
    }


    public void SetWeight(decimal? weight)
    {
        if (weight.HasValue && weight <= 0)
            throw new Exception("Weight must be greater than zero.");

        Weight = weight;
    }

    public void AssignCategory(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new Exception("Invalid category.");

        CategoryId = categoryId;
    }

    public void AssignBrand(Guid brandId)
    {
        if (brandId == Guid.Empty)
            throw new Exception("Invalid brand.");

        BrandId = brandId;
    }

    public void SetStock(int stock)
    {
        if (stock < 0)
            throw new InvalidStockException();

        Stock = stock;
    }

    public bool IsInStock() => Stock > 0;
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        Stock += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (Stock < quantity)
            throw new InvalidStockException();

        Stock -= quantity;
    }


    public void Archive()
    {
        if (Status == ProductStatus.Archived)
            throw new ProductAlreadyArchivedException();

        Status = ProductStatus.Archived;
    }

    public void UnArchive()
    {
        if (Status != ProductStatus.Archived)
            throw new ProductNotArchivedException();

        Status = ProductStatus.Published;
    }




    public void ApplyDiscount(
    decimal percentage,
    DateTime startDateUtc,
    DateTime endDateUtc)
    {
        if (percentage <= 0 || percentage > 100)
            throw new InvalidDiscountException();

        if (endDateUtc <= startDateUtc)
            throw new InvalidDiscountPeriodException();

        if (HasDiscount)
            throw new ProductAlreadyHasDiscountException();

        DiscountPercentage = percentage;
        DiscountStartDateUtc = startDateUtc;
        DiscountEndDateUtc = endDateUtc;

    }

    public void RemoveDiscount()
    {
        if (!HasDiscount)
            throw new ProductHasNoDiscountException();
        DiscountPercentage = null;
        DiscountStartDateUtc = null;
        DiscountEndDateUtc = null;
    }


}