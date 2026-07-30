using E_Commerce.Application.Features.PaymentFeature.Commands.CancelPayment;
using E_Commerce.Application.Features.PaymentFeature.Commands.CreatePayment;
using E_Commerce.Application.Features.PaymentFeature.Queries.GetMyPaymentById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class PaymentsController : ControllerBase
{
    private readonly ISender _sender;

    public PaymentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment(
        CreatePaymentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyPayments(
       [FromQuery] GetMyPaymentsQuery query,
       CancellationToken cancellationToken)
    {
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{paymentId:guid}")]
    public async Task<IActionResult> GetMyPaymentById(
    Guid paymentId,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetMyPaymentByIdQuery(paymentId),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }


    [HttpPost("{paymentId:guid}/cancel")]
    public async Task<IActionResult> CancelPayment(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CancelPaymentCommand(paymentId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}