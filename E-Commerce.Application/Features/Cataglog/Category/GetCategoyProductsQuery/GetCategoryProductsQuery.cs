using E_Commerce.Application.Features.Cataglog.Category.DTO;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Category.Queries.GetCategoryProducts;

public sealed record GetCategoryProductsQuery(Guid CategoryId)
    : IRequest<List<CategoryProductDto>>;