using E_Commerce.Application.Features.Cataglog.Product.DTO;
using MediatR;

namespace E_Commerce.Application.Features.Cataglog.Product.Quereis.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<List<ProductDto>>;
}
