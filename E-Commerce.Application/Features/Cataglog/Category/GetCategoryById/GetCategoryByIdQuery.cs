using E_Commerce.Application.Features.Cataglog.Category.DTO;
using MediatR;


namespace E_Commerce.Application.Features.Cataglog.Category.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
}
