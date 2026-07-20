
using E_Commerce.Application.Features.Catalog.DTOs;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrands;

public sealed record GetBrandsQuery()
    : IRequest<List<BrandDto>>;