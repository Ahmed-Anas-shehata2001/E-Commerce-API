using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
 /// <summary>
    /// Handles disabling two-factor authentication.
    /// </summary>
    public class DisableTwoFactorCommandHandler : IRequestHandler<DisableTwoFactorCommand, Result>
    {
 private readonly IAuthenticationService _authenticationService;

   public DisableTwoFactorCommandHandler(IAuthenticationService authenticationService)
        {
 _authenticationService = authenticationService;
        }

      public async Task<Result> Handle(DisableTwoFactorCommand request, CancellationToken cancellationToken)
        {
     return await _authenticationService.DisableTwoFactorAsync(request.UserId, cancellationToken);
        }
    }
}
