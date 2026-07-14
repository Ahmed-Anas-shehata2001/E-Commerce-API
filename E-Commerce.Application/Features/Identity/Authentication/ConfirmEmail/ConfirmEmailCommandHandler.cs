using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Handles email confirmation.
    /// </summary>
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
   {
   private readonly IAuthenticationService _authenticationService;

  public ConfirmEmailCommandHandler(IAuthenticationService authenticationService)
        {
   _authenticationService = authenticationService;
    }

        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
       return await _authenticationService.ConfirmEmailAsync(request.UserId, request.Token, cancellationToken);
      }
  }
}
