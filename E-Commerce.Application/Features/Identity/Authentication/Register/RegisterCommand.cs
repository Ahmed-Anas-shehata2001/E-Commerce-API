using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Command to register a new user.
    /// </summary>
public record RegisterCommand(
        string Email,
        string Password,
 string ConfirmPassword,
        string FirstName,
     string LastName,
        string? PhoneNumber = null
    ) : IRequest<AuthResult>;
}
