using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId)
    : IRequest<Result>;