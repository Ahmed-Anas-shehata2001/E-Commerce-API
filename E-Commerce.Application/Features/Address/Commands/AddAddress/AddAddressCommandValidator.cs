using FluentValidation;

namespace E_Commerce.Application.Features.Address.Commands.AddAddress;

public sealed class AddAddressCommandValidator
    : AbstractValidator<AddAddressCommand>
{
    public AddAddressCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.State)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Building)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .MaximumLength(20);
    }
}