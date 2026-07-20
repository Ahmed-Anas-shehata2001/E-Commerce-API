using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Features.Catalog.Exceptions;

namespace E_Commerce.Domain.Features.Catalog.ReviewFeature.Entities;

public sealed class Review : BaseEntity
{
    public Guid ProductId { get; private set; }

    public Guid CustomerId { get; private set; }

    public int Rating { get; private set; }

    public string? Comment { get; private set; }

    private Review() { }

    public Review(
        Guid productId,
        Guid customerId,
        int rating,
        string? comment)
    {
        if (productId == Guid.Empty)
            throw new InvalidReviewProductException();

        if (customerId == Guid.Empty)
            throw new InvalidReviewCustomerException();

        SetRating(rating);
        SetComment(comment);

        ProductId = productId;
        CustomerId = customerId;
    }

    public void SetRating(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new InvalidReviewRatingException();

        Rating = rating;
    }

    public void SetComment(string? comment)
    {
        Comment = comment?.Trim();
    }

    public void Update(int rating, string? comment)
    {
        SetRating(rating);
        SetComment(comment);

        UpdatedAtUtc = DateTime.UtcNow;
    }
}