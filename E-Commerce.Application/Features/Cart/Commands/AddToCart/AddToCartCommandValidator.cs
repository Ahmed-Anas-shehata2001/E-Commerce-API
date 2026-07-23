using FluentValidation;

namespace E_Commerce.Application.Features.Cart.Commands.AddToCart;

public sealed class AddToCartCommandValidator
    : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}