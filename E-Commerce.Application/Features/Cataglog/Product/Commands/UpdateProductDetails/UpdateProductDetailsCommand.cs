using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed record UpdateProductDetailsCommand(
    Guid ProductId,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    decimal? Weight,
    Guid CategoryId,
    Guid BrandId
) : IRequest<Result>;