
using E_Commerce.Application.Features.CategoryFeature.DTO;

using MediatR;


namespace E_Commerce.Application.Features.CategoryFeature.Queries
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
}
