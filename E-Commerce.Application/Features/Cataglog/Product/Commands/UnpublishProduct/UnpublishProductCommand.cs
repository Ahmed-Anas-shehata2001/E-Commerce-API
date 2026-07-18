using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.UnpublishProduct;

public sealed record UnpublishProductCommand(Guid ProductId)
    : IRequest<Result>;