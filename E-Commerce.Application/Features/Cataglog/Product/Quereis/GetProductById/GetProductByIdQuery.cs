using E_Commerce.Application.Features.Catalog.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cataglog.Product.Quereis.GetProductById;

public sealed record GetProductByIdQuery(Guid Id)
    : IRequest<Result<ProductDto>>;