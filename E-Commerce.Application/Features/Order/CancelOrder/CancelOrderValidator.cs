using FluentValidation;

namespace E_Commerce.Application.Features.Order.Commands.CancelOrder;

public sealed class CancelOrderCommandValidator
    : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();
    }
}