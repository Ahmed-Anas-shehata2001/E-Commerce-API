using FluentValidation;

namespace E_Commerce.Application.Features.Catalog.Reviews.Commands.UpdateReview;

public sealed class UpdateReviewCommandValidator
    : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.Comment)
            .MaximumLength(1000);
    }
}