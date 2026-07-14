using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
   /// <summary>
    /// Handles two-factor verification.
    /// </summary>
    public class VerifyTwoFactorCommandHandler : IRequestHandler<VerifyTwoFactorCommand, Result>
    {
private readonly IAuthenticationService _authenticationService;

        public VerifyTwoFactorCommandHandler(IAuthenticationService authenticationService)
 {
_authenticationService = authenticationService;
        }

        public async Task<Result> Handle(VerifyTwoFactorCommand request, CancellationToken cancellationToken)
        {
  var twoFactorRequest = new TwoFactorModels.TwoFactorVerifyRequest
  {
          UserId = request.UserId,
      Code = request.Code
  };

      return await _authenticationService.VerifyTwoFactorAsync(twoFactorRequest, cancellationToken);
    }
    }
}
