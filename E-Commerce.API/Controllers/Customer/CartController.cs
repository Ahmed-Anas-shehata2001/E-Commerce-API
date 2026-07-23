using E_Commerce.API.DTO;
using E_Commerce.Application.Features.Cart.Commands.AddToCart;
using E_Commerce.Application.Features.Cart.Commands.ClearCart;
using E_Commerce.Application.Features.Cart.Commands.MoveWishlistToCart;
using E_Commerce.Application.Features.Cart.Commands.RemoveFromCart;
using E_Commerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;
using E_Commerce.Application.Features.Cart.Queries.GetCartItemCount;
using E_Commerce.Application.Features.Cart.Queries.GetCartTotal;
using E_Commerce.Application.Features.Cart.Queries.GetMyCart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer;

[ApiController]
[Route("api/cart")]
[Authorize]
public sealed class CartController : ControllerBase
{
    private readonly ISender _mediator;

    public CartController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get the current user's cart.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetMyCart(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetMyCartQuery(),
            ct);

        return Ok(result);
    }

    /// <summary>
    /// Get total quantity of items in the cart.
    /// </summary>
    [HttpGet("count")]
    public async Task<IActionResult> GetCartItemCount(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetCartItemCountQuery(),
            ct);

        return Ok(result);
    }

    /// <summary>
    /// Get total cart price.
    /// </summary>
    [HttpGet("total")]
    public async Task<IActionResult> GetCartTotal(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetCartTotalQuery(),
            ct);

        return Ok(result);
    }

    /// <summary>
    /// Add a product to the cart.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddToCart(
       [FromBody] AddToCartCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Update product quantity.
    /// </summary>
    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateQuantity(
        Guid productId,
        [FromBody] UpdateCartItemQuantityRequest request,
        CancellationToken ct)
    {
        var command = new UpdateCartItemQuantityCommand(
            productId,
            request.Quantity);

        var result = await _mediator.Send(command, ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Remove a product from the cart.
    /// </summary>
    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> RemoveFromCart(
        Guid productId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new RemoveFromCartCommand(productId),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Clear the cart.
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> ClearCart(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new ClearCartCommand(),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Move all wishlist items into the cart.
    /// </summary>
    [HttpPost("move-wishlist")]
    public async Task<IActionResult> MoveWishlistToCart(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new MoveWishlistToCartCommand(),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }
}