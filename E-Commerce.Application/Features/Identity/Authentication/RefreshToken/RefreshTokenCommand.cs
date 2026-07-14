using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Command to refresh an expired access token.
    /// </summary>
    public record RefreshTokenCommand(
   string RefreshToken
    ) : IRequest<AuthResult>;
}
