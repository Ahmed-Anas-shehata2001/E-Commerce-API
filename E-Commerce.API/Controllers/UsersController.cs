using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Identity.Users;
using E_Commerce.Application.Features.Identity.Users.Commands.AssignRoleToUser;
using E_Commerce.Application.Features.Identity.Users.Queries.GetUserByEmail;
using E_Commerce.Application.Features.Identity.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(
        Guid id,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new GetUserByIdQuery(id),
            ct);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpGet("by-email")]
    public async Task<IActionResult> GetUserByEmail(
        [FromQuery] string email,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new GetUserByEmailQuery(email),
            ct);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken ct)
    {
        var result = await _sender.Send(new GetCurrentUserQuery(), ct);

        if (!result.IsSuccess)
            return Unauthorized(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(
    [FromQuery] GetUsersQuery query,
    CancellationToken ct)
    {
        var result = await _sender.Send(query, ct);

        return Ok(result);
    }





    [HttpGet("{id:guid}/permissions")]
    public async Task<IActionResult> GetUserPermissions(
    Guid id,
    CancellationToken ct)
    {
        var result = await _sender.Send(
            new GetUserPermissionsQuery(id),
            ct);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUser(
    Guid userId,
    UpdateUserCommand command,
    CancellationToken cancellationToken)
    {
        if (userId != command.UserId)
            return BadRequest("There's a problem in User id");

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(
    Guid userId,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new DeleteUserCommand(userId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpPost("{userId:guid}/lock")]
    public async Task<IActionResult> LockUser(
    Guid userId,
    LockUserRequest request,
    CancellationToken cancellationToken)
    {
        var command = new LockUserCommand(
            userId,
            request.LockoutEndUtc);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpPost("{userId:guid}/unlock")]
    public async Task<IActionResult> UnlockUser(
    Guid userId,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new UnlockUserCommand(userId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpGet("{id:guid}/roles")]
    public async Task<IActionResult> GetUserRoles(
Guid id,
CancellationToken ct)
    {
        var result = await _sender.Send(
            new GetUserRolesQuery(id),
            ct);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpGet("{userId:guid}/roles/{roleName}")]
    public async Task<IActionResult> IsUserInRole(
        Guid userId,
        string roleName,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new IsUserInRoleQuery(userId, roleName),
            cancellationToken);

        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

    [HttpPost("{userId:guid}/roles")]
    public async Task<IActionResult> AssignRole(
    Guid userId,
    [FromBody] AssignRoleToUserCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command with { UserId = userId },
            cancellationToken);

        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

    [HttpDelete("{userId:guid}/roles/{roleName}")]
    public async Task<IActionResult> RemoveRole(
    Guid userId,
    string roleName,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new RemoveRoleFromUserCommand(userId, roleName),
            cancellationToken);

        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

    [HttpPut("{userId:guid}/roles")]
    public async Task<IActionResult> UpdateRoles(
    Guid userId,
    [FromBody] UpdateUserRolesCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command with { UserId = userId },
            cancellationToken);

        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

}