using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
  /// Command to confirm a user's email address.
    /// </summary>
    public record ConfirmEmailCommand(
    string UserId,
        string Token
    ) : IRequest<Result>;
}
