using E_Commerce.Application.Features.Cataglog.Category.CreateCategory;
using E_Commerce.Application.Features.Cataglog.Category.UpdateProduct;
using E_Commerce.Application.Features.CategoryFeature.GetAllProducts;
using E_Commerce.Application.Features.CategoryFeature.GetCategoryById;
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
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // get all categories endpoint
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryCommand command)
        {
            var fixedCommand = new UpdateCategoryCommand(id, command.Name);

            var result = await _mediator.Send(fixedCommand);

            return result.IsFailure
                ? NotFound(result.Error)
                : NoContent();
        }

    }
}
