using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
  /// <summary>
    /// Command to disable two-factor authentication.
    /// </summary>
   public record DisableTwoFactorCommand(
        string UserId
  ) : IRequest<Result>;
}
