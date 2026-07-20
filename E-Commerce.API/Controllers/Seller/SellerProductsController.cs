using E_Commerce.API.DTO;
using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerProducts;
using E_Commerce.Application.Features.Catalog.Commands.CreateProduct;
using E_Commerce.Application.Features.Catalog.Commands.DeleteProduct;
using E_Commerce.Application.Features.Catalog.Commands.PublishProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Seller;

[ApiController]
[Route("api/seller/products")]
[Authorize(Roles = "Seller")]
public sealed class SellerProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SellerProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a product.
    /// SellerId should come from the logged-in user, not from the request.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductCommand command,
        CancellationToken ct)
    {
        var id = await _mediator.Send(command, ct);

        return Created($"/api/products/{id}", new { id });
    }

    /// <summary>
    /// Update one of my products.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateProductRequest request,
        CancellationToken ct)
    {
        var command = new UpdateProductDetailsCommand(
             id,
             request.Name,
             request.Description,
             request.Price,
             request.Stock,
             request.Weight,
             request.CategoryId,
             request.BrandId);

        var result = await _mediator.Send(command, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    /// <summary>
    /// Archive my product.
    /// </summary>
    [HttpPatch("{id:guid}/archive")]
    public async Task<IActionResult> Archive(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new ArchiveProductCommand(id),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    /// <summary>
    /// Restore my archived product.
    /// </summary>
    [HttpPatch("{id:guid}/unarchive")]
    public async Task<IActionResult> UnArchive(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new UnArchiveProductCommand(id),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    /// <summary>
    /// Publish my product.
    /// </summary>
    [HttpPatch("{id:guid}/publish")]
    public async Task<IActionResult> Publish(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new PublishProductCommand(id),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    /// <summary>
    /// Delete my product.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new DeleteProductCommand(id),
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    /// <summary>
    /// Get all of my products.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetMyProducts(
        [FromQuery] GetSellerProductsQuery query,
        CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);

        return Ok(result);
    }

    /// <summary>
    /// Search only my products.
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] SearchProductsQuery query,
        CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);

        return Ok(result);
    }
}