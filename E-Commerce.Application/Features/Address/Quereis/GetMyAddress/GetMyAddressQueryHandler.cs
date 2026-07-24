using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Address.DTOs;
using E_Commerce.Domain.Features.AddressFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Address.Queries.GetMyAddresses;

public sealed class GetMyAddressesQueryHandler
    : IRequestHandler<GetMyAddressesQuery, List<AddressDto>>
{
    private readonly IAddressRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetMyAddressesQueryHandler(
        IAddressRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<List<AddressDto>> Handle(
        GetMyAddressesQuery request,
        CancellationToken ct)
    {
        var addresses = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId!.Value,
            ct);

        return addresses.Select(x => new AddressDto
        {
            Id = x.Id,
            FullName = x.FullName,
            PhoneNumber = x.PhoneNumber,
            Country = x.Country,
            City = x.City,
            State = x.State,
            Street = x.Street,
            Building = x.Building,
            Apartment = x.Apartment,
            PostalCode = x.PostalCode,
            IsDefault = x.IsDefault,
            CreatedAtUtc = x.CreatedAtUtc
        }).ToList();
    }
}