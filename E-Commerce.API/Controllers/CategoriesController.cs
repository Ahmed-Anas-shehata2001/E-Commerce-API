
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

namespace E_Commerce.API.Controllers
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


        // create category endpoint
        [HttpPost]
        public async Task<IActionResult> Create(
    CreateCategoryCommand command,
    CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
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

        // update category endpoint

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


        [HttpPatch("{id:guid}/archive")]
        public async Task<IActionResult> Archive(
    Guid id,
    CancellationToken ct)
        {
            var result = await _mediator.Send(
                new ArchiveCategoryCommand(id),
                ct);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result);
        }

        [HttpPatch("{id:guid}/unarchive")]
        public async Task<IActionResult> UnArchive(
            Guid id,
            CancellationToken ct)
        {
            var result = await _mediator.Send(
                new UnArchiveCategoryCommand(id),
                ct);

            return result.IsSuccess
                ? NoContent()
                : NotFound(result);
        }

    }
}
