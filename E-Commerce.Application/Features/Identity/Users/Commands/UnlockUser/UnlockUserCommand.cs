using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Users;

public record UnlockUserCommand(Guid UserId)
    : IRequest<Result>;