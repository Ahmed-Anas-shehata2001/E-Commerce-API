using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Command to reset a user's password.
    /// </summary>
   public record ResetPasswordCommand(
        string Email,
    string Token,
        string NewPassword,
     string ConfirmNewPassword
    ) : IRequest<Result>;
}
