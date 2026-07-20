using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed record UnArchiveProductCommand(Guid ProductId) : IRequest<Result>;