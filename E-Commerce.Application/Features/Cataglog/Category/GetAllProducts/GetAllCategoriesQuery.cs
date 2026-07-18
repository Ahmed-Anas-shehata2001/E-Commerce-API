using E_Commerce.Application.Features.Cataglog.Category.DTO;
using MediatR;

namespace E_Commerce.Application.Features.Cataglog.Category.GetAllProducts
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
