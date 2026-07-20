using FluentValidation;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.UpdateBrand;

public sealed class UpdateBrandCommandValidator
    : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}