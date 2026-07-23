using FluentValidation;

namespace E_Commerce.Application.Features.Cart.Commands.RemoveFromCart;

public sealed class RemoveFromCartCommandValidator
    : AbstractValidator<RemoveFromCartCommand>
{
    public RemoveFromCartCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}