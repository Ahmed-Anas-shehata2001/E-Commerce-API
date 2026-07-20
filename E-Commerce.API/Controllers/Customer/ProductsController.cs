using E_Commerce.Application.Features.Cataglog.Product.Quereis.GetProductById;
using E_Commerce.Application.Features.Catalog.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

  

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
         Guid id,
         CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetProductByIdQuery(id),
                cancellationToken);

            if (result.IsFailure)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
    [FromQuery] SearchProductsQuery query,
    CancellationToken ct)
        {
            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }




    }
}
