using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
  /// <summary>
    /// Handles password change.
    /// </summary>
 public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
    private readonly IAuthenticationService _authenticationService;

  public ChangePasswordCommandHandler(IAuthenticationService authenticationService)
{
      _authenticationService = authenticationService;
     }

  public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
   {
       var changePasswordRequest = new ChangePasswordRequest
  {
             UserId = request.UserId,
      CurrentPassword = request.CurrentPassword,
         NewPassword = request.NewPassword,
     ConfirmNewPassword = request.ConfirmNewPassword
           };

    return await _authenticationService.ChangePasswordAsync(changePasswordRequest, cancellationToken);
     }
    }
}
