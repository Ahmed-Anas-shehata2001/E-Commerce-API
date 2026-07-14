using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Handles login with two-factor authentication.
    /// </summary>
    public class LoginWithTwoFactorCommandHandler : IRequestHandler<LoginWithTwoFactorCommand, AuthResult>
{
    private readonly IAuthenticationService _authenticationService;

        public LoginWithTwoFactorCommandHandler(IAuthenticationService authenticationService)
        {
 _authenticationService = authenticationService;
      }

        public async Task<AuthResult> Handle(LoginWithTwoFactorCommand request, CancellationToken cancellationToken)
        {
          var twoFactorRequest = new TwoFactorModels.TwoFactorLoginRequest
    {
   UserId = request.UserId,
      Code = request.Code
    };

   return await _authenticationService.LoginWithTwoFactorAsync(twoFactorRequest, cancellationToken);
       }
    }
}
