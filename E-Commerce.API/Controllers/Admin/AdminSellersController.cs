
using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerProducts;
using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Admin;

[ApiController]
[Route("api/admin/sellers")]
[Authorize(Roles = "Admin")]
public sealed class AdminSellersController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminSellersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSellers(
     [FromQuery] int pageNumber = 1,
     [FromQuery] int pageSize = 20,
     CancellationToken ct = default)
    {
        var result = await _mediator.Send(
            new GetSellersQuery(pageNumber, pageSize),
            ct);

        return Ok(result);
    }

    [HttpGet("{sellerId:guid}")]
    public async Task<IActionResult> GetSellerById(
        Guid sellerId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetSellerByIdQuery(sellerId),
            ct);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{sellerId:guid}/statistics")]
    public async Task<IActionResult> GetStatistics(
        Guid sellerId,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetSellerStatisticsQuery(sellerId),
            ct);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);

    }



    [HttpGet("{sellerId:guid}/products")]
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




}