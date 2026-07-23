using FluentValidation;

namespace E_Commerce.Application.Features.Wishlist.Commands.RemoveFromWishlist;

public sealed class RemoveFromWishlistCommandValidator
    : AbstractValidator<RemoveFromWishlistCommand>
{
    public RemoveFromWishlistCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}