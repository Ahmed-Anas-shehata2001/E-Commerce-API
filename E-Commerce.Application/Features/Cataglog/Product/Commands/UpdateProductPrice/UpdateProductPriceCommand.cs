using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.UpdateProductPrice;

public sealed record UpdateProductPriceCommand(
    Guid ProductId,
    decimal Price
) : IRequest<Result>;