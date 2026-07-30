using FluentValidation;

namespace E_Commerce.Application.Features.PaymentFeature.Queries.GetPayments;

public sealed class GetPaymentsQueryValidator
    : AbstractValidator<GetPaymentsQuery>
{
    public GetPaymentsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}