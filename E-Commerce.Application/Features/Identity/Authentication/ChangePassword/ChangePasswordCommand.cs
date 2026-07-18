using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Command to change user's password.
    /// </summary>
  public record ChangePasswordCommand(
    Guid UserId,
   string CurrentPassword,
        string NewPassword,
   string ConfirmNewPassword
    ) : IRequest<Result>;
}
