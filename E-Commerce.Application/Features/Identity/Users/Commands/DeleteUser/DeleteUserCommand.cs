using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Users;

public record DeleteUserCommand(Guid UserId)
    : IRequest<Result>;