using FluentValidation;

namespace E_Commerce.Application.Features.Order.Commands.PlaceOrder;

public sealed class PlaceOrderCommandValidator
    : AbstractValidator<PlaceOrderCommand>
{
    public PlaceOrderCommandValidator()
    {
        RuleFor(x => x.AddressId)
            .NotEmpty();
    }
}