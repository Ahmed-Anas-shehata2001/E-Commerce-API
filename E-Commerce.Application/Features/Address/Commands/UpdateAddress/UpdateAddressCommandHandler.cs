using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.AddressFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Address.Commands.UpdateAddress;

public sealed class UpdateAddressCommandHandler
    : IRequestHandler<UpdateAddressCommand, Result>
{
    private readonly IAddressRepository _repository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAddressCommandHandler(
        IAddressRepository repository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateAddressCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        var address = await _repository.GetByIdAsync(
            request.AddressId,
            ct);

        if (address is null)
            return Result.Failure("Address not found.");

        if (address.CustomerId != _currentUser.UserId.Value)
            return Result.Failure("You are not allowed to update this address.");

        address.Update(
            request.FullName,
            request.PhoneNumber,
            request.Country,
            request.City,
            request.State,
            request.Street,
            request.Building,
            request.Apartment,
            request.PostalCode);

        _repository.Update(address);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}