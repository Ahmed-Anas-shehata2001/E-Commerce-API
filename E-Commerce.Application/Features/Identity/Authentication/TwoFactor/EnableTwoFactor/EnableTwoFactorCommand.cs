using MediatR;
using E_Commerce.Domain.Common.Result;
using static E_Commerce.Application.Common.Contracts.Identity.Models.TwoFactorModels;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Command to enable two-factor authentication.
    /// </summary>
    public record EnableTwoFactorCommand(
        string UserId
    ) : IRequest<Result<TwoFactorSetupResult>>;
}
