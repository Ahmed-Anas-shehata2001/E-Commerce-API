using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Cart_Feature;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public sealed class UpdateCartItemQuantityCommandHandler
    : IRequestHandler<UpdateCartItemQuantityCommand, Result>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCartItemQuantityCommandHandler(
        ICartRepository cartRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateCartItemQuantityCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        var cart = await _cartRepository.GetByCustomerIdAsync(
            _currentUser.UserId.Value,
            ct);

        if (cart is null)
            return Result.Failure("Cart not found.");

        cart.UpdateQuantity(
            request.ProductId,
            request.Quantity);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}