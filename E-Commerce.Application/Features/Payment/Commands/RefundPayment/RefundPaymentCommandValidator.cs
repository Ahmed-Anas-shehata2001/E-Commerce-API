using FluentValidation;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.RefundPayment;

public sealed class RefundPaymentCommandValidator
    : AbstractValidator<RefundPaymentCommand>
{
    public RefundPaymentCommandValidator()
    {
        RuleFor(x => x.PaymentId)
            .NotEmpty()
            .WithMessage("PaymentId is required.");
    }
}