using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Command to request a password reset.
    /// </summary>
   public record ForgotPasswordCommand(
        string Email
    ) : IRequest<Result>;
}
