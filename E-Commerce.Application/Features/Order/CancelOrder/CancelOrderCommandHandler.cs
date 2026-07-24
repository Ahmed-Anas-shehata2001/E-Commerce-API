using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using E_Commerce.Domain.Features.OrderFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Order.Commands.CancelOrder;

public sealed class CancelOrderCommandHandler
    : IRequestHandler<CancelOrderCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }


    /*
     * checks authentication
    loads the order
    ensures it belongs to the current user
    cancels the order
    restores product stock
    saves changes

     */

    public async Task<Result> Handle(
        CancelOrderCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        var order = await _orderRepository.GetByIdWithItemsAsync(
            request.OrderId,
            ct);

        if (order is null)
            return Result.Failure("Order not found.");

        if (order.CustomerId != _currentUser.UserId.Value)
            return Result.Failure("You are not allowed to cancel this order.");

        order.Cancel();

        foreach (var item in order.Items)
        {
            var product = await _productRepository.GetProductByIdAsync(
                item.ProductId,
                ct);

            if (product is null)
                continue;

            product.IncreaseStock(item.Quantity);
        }

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}