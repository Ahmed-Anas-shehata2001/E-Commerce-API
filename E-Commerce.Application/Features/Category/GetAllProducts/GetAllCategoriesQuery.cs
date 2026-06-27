using E_Commerce.Application.Features.CategoryFeature.DTO;
using MediatR;

namespace E_Commerce.Application.Features.CategoryFeature.GetAllProducts
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
