using E_Commerce.API.DTO;
using E_Commerce.Application.Features.Catalog.Categories.Commands.ArchiveCategory;
using E_Commerce.Application.Features.Catalog.Categories.Commands.CreateCategory;
using E_Commerce.Application.Features.Catalog.Categories.Commands.UnArchiveCategory;
using E_Commerce.Application.Features.Catalog.Categories.Commands.UpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Admin;

[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = "Admin")]
public sealed class AdminCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new category.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCategoryCommand command,
        CancellationToken ct)
    {
        var id = await _mediator.Send(command, ct);

        return Created($"/api/categories/{id}", new { id });
    }

    /// <summary>
    /// Update an existing category.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateCategoryRequest request,
        CancellationToken ct)
    {
        var command = new UpdateCategoryCommand(
            id,
            request.Name,
            request.Description);

        var result = await _mediator.Send(command, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    /// <summary>
    /// Archive a category.
    /// </summary>
    [HttpPatch("{id:guid}/archive")]
    public async Task<IActionResult> Archive(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new ArchiveCategoryCommand(id),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    /// <summary>
    /// Restore an archived category.
    /// </summary>
    [HttpPatch("{id:guid}/unarchive")]
    public async Task<IActionResult> UnArchive(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new UnArchiveCategoryCommand(id),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    // Optional if you support hard delete
    /*
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new DeleteCategoryCommand(id),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
    */
}