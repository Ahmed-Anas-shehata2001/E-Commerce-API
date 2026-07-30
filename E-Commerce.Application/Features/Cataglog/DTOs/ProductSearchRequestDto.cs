using E_Commerce.Application.Common.Models;

public sealed class ProductSearchRequest : PaginationRequest
{
    public Guid? CategoryId { get; init; }

    public Guid? BrandId { get; init; }

    public Guid? SellerId { get; init; }

    public decimal? MinPrice { get; init; }

    public decimal? MaxPrice { get; init; }

    public bool? PublishedOnly { get; init; }

    public string? SortBy { get; init; }

    public bool Descending { get; init; }
}