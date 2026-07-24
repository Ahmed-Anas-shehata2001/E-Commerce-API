using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.AddressFeature.Entities;
using E_Commerce.Domain.Features.AddressFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Address.Commands.AddAddress;

public sealed class AddAddressCommandHandler
    : IRequestHandler<AddAddressCommand, Result>
{
    private readonly IAddressRepository _repository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public AddAddressCommandHandler(
        IAddressRepository repository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        AddAddressCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        if (request.IsDefault)
        {
            var addresses = await _repository.GetByCustomerIdAsync(
                _currentUser.UserId.Value,
                ct);

            foreach (var address in addresses)
            {
                address.SetDefault(false);
            }
        }

        var addressEntity = new Domain.Features.AddressFeature.Entities.Address(
            _currentUser.UserId.Value,
            request.FullName,
            request.PhoneNumber,
            request.Country,
            request.City,
            request.State,
            request.Street,
            request.Building,
            request.Apartment,
            request.PostalCode,
            request.IsDefault);

        await _repository.AddAsync(addressEntity, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}