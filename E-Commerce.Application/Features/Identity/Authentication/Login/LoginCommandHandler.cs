using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Handles user login.
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
    {
        private readonly IAuthenticationService _authenticationService;

   public LoginCommandHandler(IAuthenticationService authenticationService)
   {
        _authenticationService = authenticationService;
        }

  public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
        var loginRequest = new LoginRequest
      {
 Email = request.Email,
    Password = request.Password
            };

   return await _authenticationService.LoginAsync(loginRequest, cancellationToken);
        }
    }
}
