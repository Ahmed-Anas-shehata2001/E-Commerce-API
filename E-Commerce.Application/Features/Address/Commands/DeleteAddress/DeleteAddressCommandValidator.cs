using FluentValidation;

namespace E_Commerce.Application.Features.Address.Commands.DeleteAddress;

public sealed class DeleteAddressCommandValidator
    : AbstractValidator<DeleteAddressCommand>
{
    public DeleteAddressCommandValidator()
    {
        RuleFor(x => x.AddressId)
            .NotEmpty();
    }
}