using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.AddressFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Address.Commands.SetDefaultAddress;

public sealed class SetDefaultAddressCommandHandler
    : IRequestHandler<SetDefaultAddressCommand, Result>
{
    private readonly IAddressRepository _repository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public SetDefaultAddressCommandHandler(
        IAddressRepository repository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        SetDefaultAddressCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        var addresses = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId.Value,
            ct);

        var selectedAddress = addresses
            .FirstOrDefault(x => x.Id == request.AddressId);

        if (selectedAddress is null)
            return Result.Failure("Address not found.");

        foreach (var address in addresses)
        {
            address.SetDefault(address.Id == request.AddressId);
        }

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}