using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Admin.Users.Commands.AssignRoleToUser;
using E_Commerce.Application.Features.Admin.Users.Commands.DeleteUser;
using E_Commerce.Application.Features.Admin.Users.Commands.LockUser;
using E_Commerce.Application.Features.Admin.Users.Commands.RemoveRoleFromUser;
using E_Commerce.Application.Features.Admin.Users.Commands.UnlockUser;
using E_Commerce.Application.Features.Admin.Users.Commands.UpdateUser;
using E_Commerce.Application.Features.Admin.Users.Commands.UpdateUserRoles;
using E_Commerce.Application.Features.Admin.Users.Queries.GetUserById;
using E_Commerce.Application.Features.Admin.Users.Queries.GetUserPermissions;
using E_Commerce.Application.Features.Admin.Users.Queries.GetUserRoles;
using E_Commerce.Application.Features.Admin.Users.Queries.GetUsers;
using E_Commerce.Application.Features.Admin.Users.Queries.IsUserInRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Admin;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public sealed class AdminUsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminUsersController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(
     Guid id,
     CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetUserByIdQuery(id),
            ct);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }





    [HttpGet]
    public async Task<IActionResult> GetUsers(
    [FromQuery] GetUsersQuery query,
    CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);

        return Ok(result);
    }



    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new DeleteUserCommand(id),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }




    [HttpGet("{id:guid}/roles")]
    public async Task<IActionResult> GetUserRoles(
Guid id,
CancellationToken ct)
    {
        var result = await _mediator.Send(
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
        var result = await _mediator.Send(
            new IsUserInRoleQuery(userId, roleName),
            cancellationToken);

        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

    [HttpPost("{userId:guid}/roles")]
    public async Task<IActionResult> AssignRoleToUser(
    Guid userId,
    [FromBody] AssignRoleToUserCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command with { UserId = userId },
            cancellationToken);

        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

    [HttpDelete("{userId:guid}/roles/{roleName}")]
    public async Task<IActionResult> DeleteRoleFromUser(
    Guid userId,
    string roleName,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new RemoveRoleFromUserCommand(userId, roleName),
            cancellationToken);

        return result.IsFailure ? BadRequest(result) : Ok(result);
    }

    [HttpPut("{userId:guid}/roles")]
    public async Task<IActionResult> UpdatUsereRoles(
    Guid userId,
    [FromBody] UpdateUserRolesCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command with { UserId = userId },
            cancellationToken);

        return result.IsFailure ? BadRequest(result) : Ok(result);
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

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpPost("{userId:guid}/unlock")]
    public async Task<IActionResult> UnlockUser(
    Guid userId,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new UnlockUserCommand(userId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }




    [HttpGet("{id:guid}/permissions")]
    public async Task<IActionResult> GetUserPermissions(
    Guid id,
    CancellationToken ct)
    {
        var result = await _mediator.Send(
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

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

}