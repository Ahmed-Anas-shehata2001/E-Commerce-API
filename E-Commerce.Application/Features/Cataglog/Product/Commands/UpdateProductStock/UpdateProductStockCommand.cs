using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.UpdateProductStock;

public sealed record UpdateProductStockCommand(
    Guid ProductId,
    int NewStock
) : IRequest<Result>;