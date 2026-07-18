using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string SKU,
    string? Description,
    decimal Price,
    int Stock,
    decimal? Weight,
    Guid CategoryId,
    Guid BrandId,
    Guid SellerId
) : IRequest<Guid>;