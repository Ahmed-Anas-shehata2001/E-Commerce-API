using FluentValidation;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.CreateBrand;

public sealed class CreateBrandCommandValidator
    : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}