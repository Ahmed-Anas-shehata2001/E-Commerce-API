using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Address.DTOs;
using E_Commerce.Application.Features.Address.Quereis.GetAddressById;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.AddressFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Address.Queries.GetDefaultAddress;

public sealed class GetDefaultAddressQueryHandler
    : IRequestHandler<GetDefaultAddressQuery, Result<AddressDto>>
{
    private readonly IAddressRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetDefaultAddressQueryHandler(
        IAddressRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<Result<AddressDto>> Handle(
        GetDefaultAddressQuery request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result<AddressDto>.Failure("User is not authenticated.");

        var address = await _repository.GetDefaultAsync(
            _currentUser.UserId.Value,
            ct);

        if (address is null)
            return Result<AddressDto>.Failure("Default address not found.");

        var dto = new AddressDto
        {
            Id = address.Id,
            FullName = address.FullName,
            PhoneNumber = address.PhoneNumber,
            Country = address.Country,
            City = address.City,
            State = address.State,
            Street = address.Street,
            Building = address.Building,
            Apartment = address.Apartment,
            PostalCode = address.PostalCode,
            IsDefault = address.IsDefault,
            CreatedAtUtc = address.CreatedAtUtc
        };

        return Result<AddressDto>.Success(dto);
    }
}