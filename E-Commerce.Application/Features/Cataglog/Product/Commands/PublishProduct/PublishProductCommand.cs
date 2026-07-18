using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.PublishProduct;

public sealed record PublishProductCommand(Guid ProductId)
    : IRequest<Result<Guid>>;