using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
   /// <summary>
    /// Handles password reset.
    /// </summary>
   public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
 private readonly IAuthenticationService _authenticationService;

   public ResetPasswordCommandHandler(IAuthenticationService authenticationService)
 {
    _authenticationService = authenticationService;
   }

  public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
       {
      var resetPasswordRequest = new ResetPasswordRequest
  {
        Email = request.Email,
  Token = request.Token,
   NewPassword = request.NewPassword,
       ConfirmNewPassword = request.ConfirmNewPassword
         };

     return await _authenticationService.ResetPasswordAsync(resetPasswordRequest, cancellationToken);
       }
    }
}
