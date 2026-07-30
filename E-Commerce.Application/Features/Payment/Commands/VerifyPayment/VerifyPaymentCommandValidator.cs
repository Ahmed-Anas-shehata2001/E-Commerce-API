using FluentValidation;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.VerifyPayment;

public sealed class VerifyPaymentCommandValidator
    : AbstractValidator<VerifyPaymentCommand>
{
    public VerifyPaymentCommandValidator()
    {
        RuleFor(x => x.TransactionId)
            .NotEmpty()
            .MaximumLength(200);
    }
}