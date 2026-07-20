
using E_Commerce.Application.Features.Catalog.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrandById;

public sealed record GetBrandByIdQuery(Guid Id)
    : IRequest<Result<BrandDto>>;