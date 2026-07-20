using E_Commerce.Application.Features.Catalog.Brands.Commands.ArchiveBrand;
using E_Commerce.Application.Features.Catalog.Brands.Commands.CreateBrand;
using E_Commerce.Application.Features.Catalog.Brands.Commands.UnArchiveBrand;
using E_Commerce.Application.Features.Catalog.Brands.Commands.UpdateBrand;
using E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrandById;
using E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrandProducts;
using E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BrandsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrandsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/brands
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateBrandCommand command,
        CancellationToken ct)
    {
        var id = await _mediator.Send(command, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            id);
    }

    // PUT: api/brands/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateBrandCommand command,
        CancellationToken ct)
    {
        var fixedCommand = command with { Id = id };

        var result = await _mediator.Send(fixedCommand, ct);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    // GET: api/brands
    [HttpGet]
    public async Task<IActionResult> GetBrands(
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetBrandsQuery(),
            ct);

        return Ok(result);
    }

    // GET: api/brands/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetBrandByIdQuery(id),
            ct);

        if (result.IsFailure)
            return NotFound(result);

        return Ok(result);
    }

    // GET: api/brands/{id}/products
    [HttpGet("{id:guid}/products")]
    public async Task<IActionResult> GetProducts(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetBrandProductsQuery(id),
            ct);

        return Ok(result);
    }

    // PATCH: api/brands/{id}/archive
    [HttpPatch("{id:guid}/archive")]
    public async Task<IActionResult> Archive(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new ArchiveBrandCommand(id),
            ct);

        if (result.IsFailure)
            return NotFound(result);

        return NoContent();
    }

    // PATCH: api/brands/{id}/unarchive
    [HttpPatch("{id:guid}/unarchive")]
    public async Task<IActionResult> UnArchive(
        Guid id,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new UnArchiveBrandCommand(id),
            ct);

        if (result.IsFailure)
            return NotFound(result);

        return NoContent();
    }
}