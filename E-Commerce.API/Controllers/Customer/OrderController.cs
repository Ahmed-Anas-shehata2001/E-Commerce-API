using E_Commerce.Application.Features.Order.Commands.CancelOrder;
using E_Commerce.Application.Features.Order.Commands.PlaceOrder;
using E_Commerce.Application.Features.Order.Queries.GetMyOrders;
using E_Commerce.Application.Features.Order.Queries.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer;

[ApiController]
[Authorize]
[Route("api/orders")]
public sealed class OrderController : ControllerBase
{
    private readonly ISender _mediator;

    public OrderController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Place a new order.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(
        [FromBody] PlaceOrderCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : Ok(result.Value);
    }

    /// <summary>
    /// Get all orders for the current user.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetMyOrders(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetMyOrdersQuery(),
            ct);

        return Ok(result);
    }

    /// <summary>
    /// Get order details.
    /// </summary>
    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetById(
        Guid orderId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetOrderByIdQuery(orderId),
            ct);

        return result.IsFailure
            ? NotFound(result.Error)
            : Ok(result.Value);
    }

    /// <summary>
    /// Cancel an order.
    /// </summary>
    [HttpPost("{orderId:guid}/cancel")]
    public async Task<IActionResult> CancelOrder(
        Guid orderId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new CancelOrderCommand(orderId),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }
}