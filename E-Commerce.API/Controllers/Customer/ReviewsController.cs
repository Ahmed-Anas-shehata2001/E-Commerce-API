using E_Commerce.Application.Features.Catalog.Reviews.Commands.CreateReview;
using E_Commerce.Application.Features.Catalog.Reviews.Commands.DeleteReview;
using E_Commerce.Application.Features.Catalog.Reviews.Commands.UpdateReview;
using E_Commerce.Application.Features.Catalog.Reviews.Queries.GetProductReviews;
using E_Commerce.Application.Features.Catalog.Reviews.Queries.GetReviewById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer;

[ApiController]
[Route("api/reviews")]
public sealed class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    // GET api/reviews/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetReviewByIdQuery(id),
            ct);

        if (result.IsFailure)
            return NotFound(result);

        return Ok(result);
    }

    // GET api/reviews/product/{productId}
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