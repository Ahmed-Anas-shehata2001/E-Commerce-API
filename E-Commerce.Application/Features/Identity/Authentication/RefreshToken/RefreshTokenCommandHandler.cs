using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Handles token refresh.
 /// </summary>
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResult>
    {
        private readonly IAuthenticationService _authenticationService;

  public RefreshTokenCommandHandler(IAuthenticationService authenticationService)
  {
            _authenticationService = authenticationService;
}

        public async Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
       return await _authenticationService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
        }
    }
}
