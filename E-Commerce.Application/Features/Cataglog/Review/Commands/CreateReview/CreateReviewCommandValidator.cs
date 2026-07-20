using FluentValidation;

namespace E_Commerce.Application.Features.Catalog.Reviews.Commands.CreateReview;

public sealed class CreateReviewCommandValidator
    : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.Comment)
            .MaximumLength(1000);
    }
}