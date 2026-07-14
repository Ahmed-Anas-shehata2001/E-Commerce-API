using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Identity.Roles;
using E_Commerce.Application.Features.Identity.Roles.UpdateRolePermissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/roles")]
//[Authorize(Policy = "AdminOnly")] // or your permission-based policy
[AllowAnonymous]
public class RolesController : ControllerBase
{
    private readonly ISender _mediator;

    public RolesController(ISender mediator) => _mediator = mediator;


    [HttpGet]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetAllRolesQuery(),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetRoleByIdQuery(id),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(
    CreateRoleCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(
            nameof(GetRoleById),
            new { id = result.Value.Id },
            result.Value);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRole(
    Guid id,
    UpdateRoleCommand command,
    CancellationToken cancellationToken)
    {
        if (id != command.RoleId)
            return BadRequest("Route id does not match request body id.");

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRole(
    Guid id,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteRoleCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpGet("{id:guid}/permissions")]
    public async Task<IActionResult> GetRolePermissions(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetRolePermissionsQuery(id),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/permissions")]
    public async Task<IActionResult> UpdateRolePermissions(
    Guid id,
    UpdateRolePermissionsRequest request,
    CancellationToken cancellationToken)
    {
        var command = new UpdateRolePermissionsCommand(
            id,
            request.PermissionNames);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}