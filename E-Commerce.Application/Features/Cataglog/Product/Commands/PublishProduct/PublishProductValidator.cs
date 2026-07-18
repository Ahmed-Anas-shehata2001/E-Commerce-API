using FluentValidation;

namespace E_Commerce.Application.Features.Catalog.Commands.PublishProduct;

public sealed class PublishProductCommandValidator
    : AbstractValidator<PublishProductCommand>
{
    public PublishProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}