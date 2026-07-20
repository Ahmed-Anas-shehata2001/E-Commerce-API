using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Users.Commands.UnlockUser;

public record UnlockUserCommand(Guid UserId)
    : IRequest<Result>;