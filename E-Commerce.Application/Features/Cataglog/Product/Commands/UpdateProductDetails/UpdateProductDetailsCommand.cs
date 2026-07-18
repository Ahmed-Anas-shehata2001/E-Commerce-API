using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.UpdateProductDetails;

public sealed record UpdateProductDetailsCommand(
    Guid ProductId,
    string Name,
    string? Description,
    decimal? Weight,
    Guid CategoryId,
    Guid BrandId
) : IRequest<Result>;