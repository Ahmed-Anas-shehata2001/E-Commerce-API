using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;
using static E_Commerce.Application.Common.Contracts.Identity.Models.TwoFactorModels;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Handles enabling two-factor authentication.
    /// </summary>
    public class EnableTwoFactorCommandHandler : IRequestHandler<EnableTwoFactorCommand, Result<TwoFactorSetupResult>>
    {
private readonly IAuthenticationService _authenticationService;

        public EnableTwoFactorCommandHandler(IAuthenticationService authenticationService)
        {
 _authenticationService = authenticationService;
      }

      public async Task<Result<TwoFactorSetupResult>> Handle(EnableTwoFactorCommand request, CancellationToken cancellationToken)
        {
    return await _authenticationService.EnableTwoFactorAsync(request.UserId, cancellationToken);
        }
    }
}
