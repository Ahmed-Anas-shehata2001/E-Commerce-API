using E_Commerce.Application.Features.CategoryFeature.DTO;
using MediatR;

namespace E_Commerce.Application.Features.CategoryFeature.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
