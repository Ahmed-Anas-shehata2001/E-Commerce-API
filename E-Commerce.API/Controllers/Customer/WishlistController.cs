using E_Commerce.Application.Features.Customer.Wishlist.Queries.GetMyWishlist;
using E_Commerce.Application.Features.Customer.Wishlist.Queries.GetWishlistCount;
using E_Commerce.Application.Features.Customer.Wishlist.Queries.IsProductInWishlist;
using E_Commerce.Application.Features.Wishlist.Commands.AddToWishlist;
using E_Commerce.Application.Features.Wishlist.Commands.RemoveFromWishlist;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer;

[ApiController]
[Route("api/wishlist")]
[Authorize]
public sealed class WishlistController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishlistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Add a product to the wishlist.
    /// </summary>
    [HttpPost("{productId:guid}")]
    public async Task<IActionResult> Add(
        Guid productId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new AddToWishlistCommand(productId),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Remove a product from the wishlist.
    /// </summary>
    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Remove(
        Guid productId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new RemoveFromWishlistCommand(productId),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear(CancellationToken ct)
    {
        var result = await _mediator.Send(
            new ClearWishlistCommand(),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }


    [HttpGet]
    public async Task<IActionResult> GetMyWishlist(
    CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetMyWishlistQuery(),
            ct);

        return Ok(result);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetWishlistCountQuery(),
            ct);

        return Ok(result);
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> IsInWishlist(
        Guid productId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new IsProductInWishlistQuery(productId),
            ct);

        return Ok(result);
    }
}