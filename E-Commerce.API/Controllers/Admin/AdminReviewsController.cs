using E_Commerce.Application.Features.Catalog.Reviews.Commands.CreateReview;
using E_Commerce.Application.Features.Catalog.Reviews.Commands.DeleteReview;
using E_Commerce.Application.Features.Catalog.Reviews.Commands.UpdateReview;
using E_Commerce.Application.Features.Catalog.Reviews.Queries.GetProductReviews;
using E_Commerce.Application.Features.Catalog.Reviews.Queries.GetReviewById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Admin;

[ApiController]
[Route("api/admin/reviews")]
[Authorize(Roles = "Admin")]
public sealed class AdminReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get review by id.
    /// </summary>
    /// 


    // POST api/reviews
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateReviewCommand command,
        CancellationToken ct)
    {
        var id = await _mediator.Send(command, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            id);
    }

    // PUT api/reviews/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateReviewCommand command,
        CancellationToken ct)
    {
        var fixedCommand = command with { Id = id };

        var result = await _mediator.Send(
            fixedCommand,
            ct);

        if (result.IsFailure)
            return NotFound(result);

        return NoContent();
    }

    // DELETE api/reviews/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new DeleteReviewCommand(id),
            ct);

        if (result.IsFailure)
            return NotFound(result);

        return NoContent();
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetReviewByIdQuery(id),
            ct);

        return Ok(result);
    }

    /// <summary>
    /// Get all reviews for a product.
    /// </summary>
    [HttpGet("product/{productId:guid}")]
    public async Task<IActionResult> GetProductReviews(
        Guid productId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetProductReviewsQuery(productId),
            ct);

        return Ok(result);
    }


}