using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Handles forgot password requests.
    /// </summary>
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
  {
    private readonly IAuthenticationService _authenticationService;

  public ForgotPasswordCommandHandler(IAuthenticationService authenticationService)
        {
   _authenticationService = authenticationService;
      }

  public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
   {
        return await _authenticationService.ForgotPasswordAsync(request.Email, cancellationToken);
     }
    }
}
