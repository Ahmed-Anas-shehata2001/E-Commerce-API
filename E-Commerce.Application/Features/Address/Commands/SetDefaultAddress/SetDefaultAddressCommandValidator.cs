using FluentValidation;

namespace E_Commerce.Application.Features.Address.Commands.SetDefaultAddress;

public sealed class SetDefaultAddressCommandValidator
    : AbstractValidator<SetDefaultAddressCommand>
{
    public SetDefaultAddressCommandValidator()
    {
        RuleFor(x => x.AddressId)
            .NotEmpty();
    }
}