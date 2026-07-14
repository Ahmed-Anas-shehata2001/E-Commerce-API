using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
  /// Command to login with two-factor authentication.
    /// </summary>
    public record LoginWithTwoFactorCommand(
        string UserId,
     string Code
    ) : IRequest<AuthResult>;
}
