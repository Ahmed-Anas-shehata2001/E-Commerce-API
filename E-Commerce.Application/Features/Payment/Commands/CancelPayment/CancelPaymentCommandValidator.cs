using FluentValidation;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.CancelPayment;

public sealed class CancelPaymentCommandValidator
    : AbstractValidator<CancelPaymentCommand>
{
    public CancelPaymentCommandValidator()
    {
        RuleFor(x => x.PaymentId)
            .NotEmpty()
            .WithMessage("PaymentId is required.");
    }
}