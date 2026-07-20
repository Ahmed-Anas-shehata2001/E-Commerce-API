namespace E_Commerce.API.DTO;

public sealed class UpdateProductRequest
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public Guid CategoryId { get; set; }

    public Guid BrandId { get; set; }

    public decimal? Weight { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public DateTime? DiscountStartDateUtc { get; set; }

    public DateTime? DiscountEndDateUtc { get; set; }
}