using E_Commerce.API.DTO;
using E_Commerce.Application.Features.Cataglog.Category.GetCategories;
using E_Commerce.Application.Features.Catalog.Categories.Commands.ArchiveCategory;
using E_Commerce.Application.Features.Catalog.Categories.Commands.CreateCategory;
using E_Commerce.Application.Features.Catalog.Categories.Commands.UnArchiveCategory;
using E_Commerce.Application.Features.Catalog.Categories.Commands.UpdateCategory;
using E_Commerce.Application.Features.Catalog.Categories.Queries.GetCategoryById;
using E_Commerce.Application.Features.Catalog.Category.Queries.GetCategoryProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        // mediatR
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }





        [HttpGet]
        public async Task<IActionResult> GetCategories(
      CancellationToken ct)
        {
            var result = await _mediator.Send(
                new GetCategoriesQuery(),
                ct);

            return Ok(result);
        }

        // get category by id endpoint
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(result);
        }

   

        [HttpGet("{id:guid}/products")]
        public async Task<IActionResult> GetProducts(
    Guid id,
    CancellationToken ct)
        {
            var result = await _mediator.Send(
                new GetCategoryProductsQuery(id),
                ct);

            return Ok(result);
        }


    }
}
