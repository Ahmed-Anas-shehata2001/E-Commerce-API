using FluentValidation;

namespace E_Commerce.Application.Features.Catalog.Commands.UnpublishProduct;

public sealed class UnpublishProductCommandValidator
    : AbstractValidator<UnpublishProductCommand>
{
    public UnpublishProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}