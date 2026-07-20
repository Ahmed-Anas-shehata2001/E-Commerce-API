using E_Commerce.Application.Features.Catalog.DTOs;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrandProducts;

public sealed record GetBrandProductsQuery(Guid BrandId)
    : IRequest<List<ProductDto>>;