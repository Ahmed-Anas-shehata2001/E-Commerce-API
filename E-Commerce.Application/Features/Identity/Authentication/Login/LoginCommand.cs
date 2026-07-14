using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;

namespace E_Commerce.Application.Features.Identity.Authentication
{
  /// <summary>
    /// Command to log in a user.
    /// </summary>
    public record LoginCommand(
        string Email,
        string Password
    ) : IRequest<AuthResult>;
}
