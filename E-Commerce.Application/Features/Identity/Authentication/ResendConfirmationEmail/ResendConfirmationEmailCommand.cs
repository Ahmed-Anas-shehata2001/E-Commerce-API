using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
   /// Command to resend email confirmation to a user.
    /// </summary>
   public record ResendConfirmationEmailCommand(
        string Email
  ) : IRequest<Result>;
}
