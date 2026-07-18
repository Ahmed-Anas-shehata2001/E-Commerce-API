using FluentValidation;

namespace E_Commerce.Application.Features.Catalog.Commands.UpdateProductStock;

public sealed class UpdateProductStockCommandValidator
    : AbstractValidator<UpdateProductStockCommand>
{
    public UpdateProductStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.NewStock)
            .GreaterThanOrEqualTo(0);
    }
}