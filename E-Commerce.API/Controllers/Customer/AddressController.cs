using E_Commerce.Application.Features.Address.Commands.AddAddress;
using E_Commerce.Application.Features.Address.Commands.DeleteAddress;
using E_Commerce.Application.Features.Address.Commands.SetDefaultAddress;
using E_Commerce.Application.Features.Address.Commands.UpdateAddress;
using E_Commerce.Application.Features.Address.Quereis.GetAddressById;
using E_Commerce.Application.Features.Address.Queries.GetAddressById;
using E_Commerce.Application.Features.Address.Queries.GetMyAddresses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer;

[ApiController]
[Authorize]
[Route("api/addresses")]
public sealed class AddressController : ControllerBase
{
    private readonly ISender _mediator;

    public AddressController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all addresses for the current user.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetMyAddresses(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetMyAddressesQuery(),
            ct);

        return Ok(result);
    }

    /// <summary>
    /// Get an address by its id.
    /// </summary>
    [HttpGet("{addressId:guid}")]
    public async Task<IActionResult> GetById(
        Guid addressId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetAddressByIdQuery(addressId),
            ct);

        return result.IsFailure
            ? NotFound(result.Error)
            : Ok(result.Value);
    }

    /// <summary>
    /// Get the user's default address.
    /// </summary>
    [HttpGet("default")]
    public async Task<IActionResult> GetDefault(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetDefaultAddressQuery(),
            ct);

        return result.IsFailure
            ? NotFound(result.Error)
            : Ok(result.Value);
    }

    /// <summary>
    /// Add a new address.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] AddAddressCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Update an existing address.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UpdateAddressCommand command,
        CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Delete an address.
    /// </summary>
    [HttpDelete("{addressId:guid}")]
    public async Task<IActionResult> Delete(
        Guid addressId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new DeleteAddressCommand(addressId),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Set an address as the default address.
    /// </summary>
    [HttpPut("{addressId:guid}/default")]
    public async Task<IActionResult> SetDefault(
        Guid addressId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new SetDefaultAddressCommand(addressId),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }
}