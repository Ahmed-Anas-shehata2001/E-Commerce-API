using E_Commerce.Application.Features.Catalog.Brands.Commands.ArchiveBrand;
using E_Commerce.Application.Features.Catalog.Brands.Commands.CreateBrand;
using E_Commerce.Application.Features.Catalog.Brands.Commands.UnArchiveBrand;
using E_Commerce.Application.Features.Catalog.Brands.Commands.UpdateBrand;
using E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrandById;
using E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrandProducts;
using E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer;

[ApiController]
[Route("api/[controller]")]
public sealed class BrandsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrandsController(IMediator mediator)
    {
        _mediator = mediator;
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


}