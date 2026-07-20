using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed record ArchiveProductCommand(Guid ProductId) : IRequest<Result>;