using FluentValidation;

namespace E_Commerce.Application.Features.Catalog.Commands.UpdateProductPrice;

public sealed class UpdateProductPriceCommandValidator
    : AbstractValidator<UpdateProductPriceCommand>
{
    public UpdateProductPriceCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}