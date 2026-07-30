using FluentValidation;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.CreatePayment;

public sealed class CreatePaymentCommandValidator
    : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId is required.");
    }
}