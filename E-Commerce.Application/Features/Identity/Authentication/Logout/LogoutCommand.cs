using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Authentication
{
    /// <summary>
    /// Command to log out a user.
    /// </summary>
    public record LogoutCommand(
        Guid UserId
   ) : IRequest<Result>;
}
