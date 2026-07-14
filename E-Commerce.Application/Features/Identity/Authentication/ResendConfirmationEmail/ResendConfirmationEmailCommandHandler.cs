using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Handles resending email confirmation.
    /// </summary>
 public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand, Result>
    {
    private readonly IAuthenticationService _authenticationService;

    public ResendConfirmationEmailCommandHandler(IAuthenticationService authenticationService)
        {
 _authenticationService = authenticationService;
  }

        public async Task<Result> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
   {
      return await _authenticationService.ResendConfirmationEmailAsync(request.Email, cancellationToken);
        }
   }
}
