using MediatR;
using E_Commerce.Domain.Common.Result;
using static E_Commerce.Application.Common.Contracts.Identity.Models.TwoFactorModels;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Command to verify two-factor authentication code.
    /// </summary>
    public record VerifyTwoFactorCommand(
        string UserId,
      string Code
  ) : IRequest<Result>;
}
