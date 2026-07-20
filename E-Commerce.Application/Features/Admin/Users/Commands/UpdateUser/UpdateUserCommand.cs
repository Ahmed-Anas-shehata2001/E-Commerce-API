using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string? PhoneNumber
) : IRequest<Result>;