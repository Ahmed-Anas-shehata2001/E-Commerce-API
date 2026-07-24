using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.AddressFeature.Interfaces;
using E_Commerce.Domain.Features.Cart_Feature;
using E_Commerce.Domain.Features.OrderFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Order.Commands.PlaceOrder;

public sealed class PlaceOrderCommandHandler
    : IRequestHandler<PlaceOrderCommand, Result<Guid>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly ICartRepository _cartRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PlaceOrderCommandHandler(
        ICurrentUserService currentUser,
        ICartRepository cartRepository,
        IAddressRepository addressRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _cartRepository = cartRepository;
        _addressRepository = addressRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        PlaceOrderCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result<Guid>.Failure("User is not authenticated.");

        // Load cart
        var cart = await _cartRepository.GetByCustomerIdAsync(
            _currentUser.UserId.Value,
            ct);

        if (cart is null || !cart.Items.Any())
            return Result<Guid>.Failure("Cart is empty.");

        // Load address
        var address = await _addressRepository.GetByIdAsync(
            request.AddressId,
            ct);

        if (address is null)
            return Result<Guid>.Failure("Address not found.");

        if (address.CustomerId != _currentUser.UserId.Value)
            return Result<Guid>.Failure("Invalid address.");

        // Create order
        var order = new Domain.Features.OrderFeature.Entities.Order(
            _currentUser.UserId.Value,
            address.Id,
            address.FullName,
            address.PhoneNumber,
            address.Country,
            address.City,
            address.State,
            address.Street,
            address.Building,
            address.Apartment,
            address.PostalCode);

        foreach (var item in cart.Items)
        {
            var product = item.Product;

            if (product.Stock < item.Quantity)
                return Result<Guid>.Failure(
                    $"'{product.Name}' does not have enough stock.");

            order.AddItem(
                product.Id,
                product.Name,
                product.CurrentPrice,
                item.Quantity);

            product.DecreaseStock(item.Quantity);
        }

        await _orderRepository.AddAsync(order, ct);

        cart.Clear();

        await _unitOfWork.SaveChangesAsync(ct);

        return Result<Guid>.Success(order.Id);
    }
}