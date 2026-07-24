using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Order.DTOs;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.OrderFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Order.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler
    : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetOrderByIdQueryHandler(
        IOrderRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<Result<OrderDto>> Handle(
        GetOrderByIdQuery request,
        CancellationToken ct)
    {
        var order = await _repository.GetByIdWithItemsAsync(
            request.OrderId,
            ct);

        if (order is null)
            return Result<OrderDto>.Failure("Order not found.");

        if (order.CustomerId != _currentUser.UserId)
            return Result<OrderDto>.Failure("You do not have access to this order.");

        var dto = new OrderDto
        {
            Id = order.Id,
            Status = order.Status,
            OrderedAtUtc = order.OrderedAtUtc,
            TotalItems = order.CalculateTotalItems(),
            TotalPrice = order.CalculateTotalPrice(),

            RecipientName = order.RecipientName,
            PhoneNumber = order.PhoneNumber,
            Country = order.Country,
            City = order.City,
            State = order.State,
            Street = order.Street,
            Building = order.Building,
            Apartment = order.Apartment,
            PostalCode = order.PostalCode,

            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                TotalPrice = i.TotalPrice
            }).ToList()
        };

        return Result<OrderDto>.Success(dto);
    }
}