using E_Commerce.Application.Features.PaymentFeature.Commands.RefundPayment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/payments")]
public sealed class PaymentsController : ControllerBase
{
    private readonly ISender _sender;

    public PaymentsController(ISender sender)
    {
        _sender = sender;
    }



    [HttpGet]
    public async Task<IActionResult> GetPayments(
        [FromQuery] GetPaymentsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);


    }


    [HttpGet("{paymentId:guid}")]
    public async Task<IActionResult> GetPaymentById(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetPaymentByIdQuery(paymentId),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("{paymentId:guid}/refund")]
    public async Task<IActionResult> RefundPayment(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new RefundPaymentCommand(paymentId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}