using E_Commerce.Application.Features.PaymentFeature.Commands.VerifyPayment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Public;

[AllowAnonymous]
[ApiController]
[Route("api/payments")]
public sealed class PaymentsController : ControllerBase
{
    private readonly ISender _sender;

    public PaymentsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Paymob Webhook
    /// </summary>
    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook(
        [FromBody] VerifyPaymentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest();

        return Ok();
    }

    /// <summary>
    /// Paymob Redirect Callback
    /// </summary>
    [HttpGet("callback")]
    public async Task<IActionResult> Callback(
        [FromQuery] string transactionId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new VerifyPaymentCommand(transactionId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(new
        {
            Message = "Payment verified successfully."
        });
    }
}