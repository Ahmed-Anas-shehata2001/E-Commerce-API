using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerProducts;
using E_Commerce.Application.Features.Cataglog.Product.Quereis.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Admin;

[ApiController]
[Route("api/admin/products")]
[Authorize(Roles = "Admin")]
public sealed class AdminProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Search all products.
    /// Includes archived products if your query supports it.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Search(
        [FromQuery] SearchProductsQuery query,
        CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);

        return Ok(result);
    }

    /// <summary>
    /// Get a product by id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetProductByIdQuery(id),
            ct);

        return result.IsFailure
            ? NotFound(result.Error)
            : Ok(result.Value);
    }

    /// <summary>
    /// View all products belonging to a seller.
    /// </summary>
    [HttpGet("seller/{sellerId:guid}")]
    public async Task<IActionResult> GetSellerProducts(
        Guid sellerId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var result = await _mediator.Send(
            new GetSellerProductsQuery(
                sellerId,
                pageNumber,
                pageSize),
            ct);

        return Ok(result);
    }

    /// <summary>
    /// Archive any product.
    /// </summary>
    [HttpPatch("{id:guid}/archive")]
    public async Task<IActionResult> Archive(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new ArchiveProductCommand(id),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    /// <summary>
    /// Restore an archived product.
    /// </summary>
    [HttpPatch("{id:guid}/unarchive")]
    public async Task<IActionResult> UnArchive(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new UnArchiveProductCommand(id),
            ct);

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }



}