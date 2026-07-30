using FluentValidation;

public sealed class GetPaymentByIdQueryValidator
    : AbstractValidator<GetPaymentByIdQuery>
{
    public GetPaymentByIdQueryValidator()
    {
        RuleFor(x => x.PaymentId).NotEmpty();
    }
}