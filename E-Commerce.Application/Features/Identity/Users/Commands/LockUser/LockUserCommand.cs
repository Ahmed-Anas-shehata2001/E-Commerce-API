using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Users;

public record LockUserCommand(
    Guid UserId,
    DateTimeOffset? LockoutEndUtc
) : IRequest<Result>;