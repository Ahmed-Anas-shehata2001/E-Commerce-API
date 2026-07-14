using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Users;

public record UpdateUserCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string? PhoneNumber
) : IRequest<Result>;