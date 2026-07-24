using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Address.DTOs;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.AddressFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Address.Queries.GetAddressById;

public sealed class GetAddressByIdQueryHandler
    : IRequestHandler<GetAddressByIdQuery, Result<AddressDto>>
{
    private readonly IAddressRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetAddressByIdQueryHandler(
        IAddressRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<Result<AddressDto>> Handle(
        GetAddressByIdQuery request,
        CancellationToken ct)
    {
        var address = await _repository.GetByIdAsync(
            request.AddressId,
            ct);

        if (address is null)
            return Result<AddressDto>.Failure("Address not found.");

        if (address.CustomerId != _currentUser.UserId)
            return Result<AddressDto>.Failure("You are not allowed to view this address.");

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