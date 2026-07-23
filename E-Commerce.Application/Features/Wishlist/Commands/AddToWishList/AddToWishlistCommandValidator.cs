using FluentValidation;

namespace E_Commerce.Application.Features.Wishlist.Commands.AddToWishlist;

public sealed class AddToWishlistCommandValidator
    : AbstractValidator<AddToWishlistCommand>
{
    public AddToWishlistCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}