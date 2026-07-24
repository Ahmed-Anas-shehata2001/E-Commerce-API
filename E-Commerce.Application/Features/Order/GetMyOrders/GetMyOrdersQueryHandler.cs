using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Order.DTOs;
using E_Commerce.Domain.Features.OrderFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Order.Queries.GetMyOrders;

public sealed class GetMyOrdersQueryHandler
    : IRequestHandler<GetMyOrdersQuery, List<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetMyOrdersQueryHandler(
        IOrderRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<List<OrderDto>> Handle(
        GetMyOrdersQuery request,
        CancellationToken ct)
    {
        var orders = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId!.Value,
            ct);

        return orders.Select(x => new OrderDto
        {
            Id = x.Id,
            Status = x.Status,
            OrderedAtUtc = x.OrderedAtUtc,
            TotalItems = x.CalculateTotalItems(),
            TotalPrice = x.CalculateTotalPrice(),

            RecipientName = x.RecipientName,
            PhoneNumber = x.PhoneNumber,
            Country = x.Country,
            City = x.City,
            State = x.State,
            Street = x.Street,
            Building = x.Building,
            Apartment = x.Apartment,
            PostalCode = x.PostalCode,

            Items = x.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                TotalPrice = i.TotalPrice
            }).ToList()

        }).ToList();
    }
}