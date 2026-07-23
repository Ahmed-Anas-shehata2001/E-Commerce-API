using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed record ClearWishlistCommand : IRequest<Result>;