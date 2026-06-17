using E_Commerce.Application.Features.ProductFeature.DTO;
using MediatR;

namespace E_Commerce.Application.Features.ProductFeature.Queries
{
    public record GetAllProductsQuery : IRequest<List<ProductDto>>;
}
